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
        public bool activeWhileCarried = false;

        // State
        bool isActive = false;
        bool isStarting = false;
        float currentCooldown = 0f;
        float currentStartUp = 0f;
        [SerializeField]
        Consumer[] consumers;
        Producer[] producers;

        // Query
        public virtual bool MeetsRequirements => consumers.All(c => c.CanActivate);

        public bool IsActive => isActive;

        void Start()
        {
            consumers = GetComponents<Consumer>();
            producers = GetComponents<Producer>();
        }


        void Update()
        {
            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
                if (currentCooldown < 0)
                {
                    currentCooldown = 0;
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
                    currentCooldown = cooldown;
                    isActive = true;
                    isStarting = false;
                    Activate();
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

                    // TODO repalce with animation
                    gameObject.transform.GetComponentInChildren<SpriteRenderer>().color = new Color(0.5f, 1f, 1f);
                }
                else
                {
                    // TODO display message/animation for missing requirements
                    Debug.Log("Equipment not calibrated - examine formation for specs");
                }
            }
            else if ((isActive || isStarting) && type != InteractionType.ONCE)
            {
                isActive = false;
                isStarting = false;
                Deactivate();
            }
        }


        public virtual void Activate()
        {
            if (MeetsRequirements == false)
            {
                isActive = false;
                isStarting = false;
                return;
            }

            foreach (var consumer in consumers)
            {
                consumer.Activate();
            }
            foreach (var producer in producers)
            {
                producer.Activate();
            }

            // TODO repalce with animation
            gameObject.transform.GetComponentInChildren<SpriteRenderer>().color = new Color(0.5f, 1f, 0.75f);
        }

        public virtual void Deactivate()
        {
            foreach (var consumer in consumers)
            {
                consumer.Deactivate();
            }
            foreach (var producer in producers)
            {
                producer.Deactivate();
            }

            // TODO repalce with animation
            gameObject.transform.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        }
    }
}