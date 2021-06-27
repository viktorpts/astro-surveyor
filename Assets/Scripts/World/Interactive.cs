using System.Linq;
using UnityEngine;


namespace AstroSurveyor
{
    public abstract class Interactive : MonoBehaviour
    {
        // Config
        public InteractionType type = InteractionType.TOGGLE;
        public float cooldown = 0f;
        public float delay = 0f;
        public float startUpTime = 0f;
        public bool mustCarry = false;

        // State
        bool isActive = false;
        bool isStarting = false;
        bool IsStarting
        {
            get => isStarting;
            set
            {
                if (value == false) {
                    GameManager.Instance.HideProgressBar(gameObject);
                }
                isStarting = value;
            }
        }
        float currentCooldown = 0f;
        float currentStartUp = 0f;
        Consumer[] consumers;
        Producer[] producers;
        Animator animator;

        public bool IsPowered => isActive || isStarting;

        // Query
        public bool ConsumersSatifsfied()
        {
            var check = consumers.Where(c => !c.CanActivate).ToArray();
            if (check.Length > 0)
            {
                foreach (var missing in check)
                {
                    GameManager.Instance.ShowMessage($"Missing resource {missing.rate}x {missing.resourceType}");
                }
            }
            return check.Length == 0;
        }
        public virtual bool MeetsRequirements()
        {
            return true;
        }

        public bool IsActive => isActive;

        void Start()
        {
            consumers = GetComponents<Consumer>();
            producers = GetComponents<Producer>();
            animator = GetComponentInChildren<Animator>();
        }


        void Update()
        {
            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
                if (currentCooldown < 0)
                {
                    currentCooldown = 0;
                    if (type == InteractionType.PUSH)
                    {
                        Deactivate();
                    }
                }
            }
            if (IsStarting)
            {
                if (currentStartUp > 0)
                {
                    currentStartUp -= Time.deltaTime;
                    GameManager.Instance.UpdateProgressBar(gameObject, (startUpTime - currentStartUp) / startUpTime);

                    if (currentStartUp < 0)
                    {
                        currentStartUp = 0;
                    }
                }
                if (currentStartUp == 0)
                {
                    Activate(false);
                }
            }
            if ((IsStarting || isActive) && ConsumersSatifsfied() == false)
            {
                Deactivate();
            }
        }

        public void OnInteract()
        {
            if (!isActive && !IsStarting && currentCooldown == 0)
            {
                if (MeetsRequirements() && ConsumersSatifsfied())
                {
                    IsStarting = true;
                    currentStartUp = startUpTime;

                    if (animator != null)
                    {
                        animator.SetBool("stowed", false);
                        animator.SetBool("working", true);
                    }
                }
            }
            else if ((isActive || IsStarting) && type != InteractionType.ONCE)
            {
                Deactivate();
            }
        }


        public virtual void Activate(bool isScanner)
        {
            var requirements = false;
            if (this.GetType() == typeof(Formation))
            {
                requirements = ((Formation)this).scannerRequired == false || isScanner;
            }
            else
            {
                requirements = MeetsRequirements();
            }

            if (requirements == false || ConsumersSatifsfied() == false)
            {
                isActive = false;
                IsStarting = false;
                return;
            }

            currentCooldown = cooldown;
            isActive = true;
            IsStarting = false;

            foreach (var consumer in consumers)
            {
                consumer.Activate();
            }
            foreach (var producer in producers)
            {
                producer.Activate();
            }

            if (animator != null)
            {
                animator.SetBool("working", false);
                animator.SetBool("stowed", false);
            }
        }

        public virtual void Deactivate()
        {
            isActive = false;
            IsStarting = false;

            foreach (var consumer in consumers)
            {
                consumer.Deactivate();
            }
            foreach (var producer in producers)
            {
                producer.Deactivate();
            }

            if (animator != null)
            {
                animator.SetBool("working", false);
            }
        }

        public virtual void Stow()
        {
            Deactivate();
            if (animator != null)
            {
                animator.SetBool("stowed", true);
            }
        }
    }
}