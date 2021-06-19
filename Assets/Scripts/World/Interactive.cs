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
        private bool isActive = false;
        private bool isStarting = false;
        private float currentCooldown = 0f;
        private float currentStartUp = 0f;

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
                isStarting = true;
                currentStartUp = startUpTime;
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