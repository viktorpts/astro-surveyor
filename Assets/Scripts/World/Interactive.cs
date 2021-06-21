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
        float currentCooldown = 0f;
        float currentStartUp = 0f;
        Consumer[] consumers;
        Producer[] producers;
        Animator animator;

        // Query
        public virtual bool MeetsRequirements => consumers.All(c => c.CanActivate);

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
            if (isStarting)
            {
                if (currentStartUp > 0)
                {
                    currentStartUp -= Time.deltaTime;
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
        }

        public void OnInteract()
        {
            if (!isActive && !isStarting && currentCooldown == 0)
            {
                if (MeetsRequirements)
                {
                    isStarting = true;
                    currentStartUp = startUpTime;

                    if (animator != null)
                    {
                        animator.SetBool("stowed", false);
                        animator.SetBool("working", true);
                    }
                    else
                    {
                        gameObject.transform.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 0.5f);
                    }
                }
                else
                {
                    // TODO display message/animation for missing requirements
                    Debug.Log("Equipment not calibrated - examine formation for specs");
                }
            }
            else if ((isActive || isStarting) && type != InteractionType.ONCE)
            {
                Deactivate();
            }
        }


        public virtual void Activate(bool isScanner)
        {
            if (MeetsRequirements == false && isScanner == false)
            {
                isActive = false;
                isStarting = false;
                return;
            }

            currentCooldown = cooldown;
            isActive = true;
            isStarting = false;

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
            else
            {
                gameObject.transform.GetComponentInChildren<SpriteRenderer>().color = new Color(0.5f, 1f, 0.75f);
            }
        }

        public virtual void Deactivate()
        {
            isActive = false;
            isStarting = false;

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
            else
            {
                gameObject.transform.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f);
            }
        }

        public virtual void Stow() {
            Deactivate();
            if (animator != null)
            {
                animator.SetBool("stowed", true);
            }
        }
    }
}