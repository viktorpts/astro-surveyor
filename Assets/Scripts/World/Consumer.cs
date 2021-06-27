using UnityEngine;


namespace AstroSurveyor
{
    public class Consumer : Target
    {
        // Config
        public ResourceType resourceType;
        public int rate = 1;

        // State
        [SerializeField]
        private Producer producer;

        // Query
        [SerializeField]
        public bool CanActivate
        {
            get
            {
                if (producer != null)   // If producer is already connected, then activation is possible
                {
                    return true;
                }
                else
                {
                    return hasTarget && target.GetComponent<Producer>().IsActive && target.GetComponent<Producer>().AvailableCapacity >= rate;
                }
            }
        }

        protected override bool FilterTargets(Collider2D collider)
        {
            var targetComponent = collider.GetComponentInParent<Producer>();
            return targetComponent != null && targetComponent.gameObject != gameObject && targetComponent.resourceType == resourceType && targetComponent.AvailableCapacity >= rate;
        }

        public bool Activate()
        {
            if (hasTarget && target.GetComponent<Producer>().Link(this))
            {
                producer = target.GetComponent<Producer>();
                return true;
            }

            return false;
        }

        public void Deactivate()
        {
            if (producer != null)
            {
                producer.UnLink(this);
                producer = null;
            }
            var core = GetComponent<Interactive>();
            if (core != null && core.IsPowered) {
                core.Deactivate();
            }
        }
    }
}