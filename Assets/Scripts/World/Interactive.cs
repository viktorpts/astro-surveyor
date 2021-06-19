using UnityEngine;


namespace AstroSurveyor
{
    public abstract class Interactive : MonoBehaviour
    {
        // Config
        public InteractionType type = InteractionType.TOGGLE;
        public float cooldown = 0f;
        public float delay = 0f;
        public bool activeWhileCarried = false;

        // State
        private bool isActive = false;
        private bool isStarting = false;
        private float currentCooldown = 0f;
        private float currentDelay = 0f;

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
                if (currentDelay > 0)
                {
                    currentDelay -= Time.deltaTime;
                    if (currentDelay < 0)
                    {
                        currentDelay = 0;
                    }
                }
                if (currentDelay == 0)
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
                isStarting = true;
                currentDelay = delay;
            }
            else if ((isActive || isStarting) && type != InteractionType.ONCE)
            {
                isActive = false;
                isStarting = false;
                Deactivate();
            }
        }

        public abstract void Activate();

        public abstract void Deactivate();
    }
}