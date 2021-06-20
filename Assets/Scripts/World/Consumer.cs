using UnityEngine;


namespace AstroSurveyor
{
    public class Consumer : Target
    {
        // Config
        public ResourceType resourceType;
        public int rate = 1;

        // State
        private Producer producer;

        // Query
        [SerializeField]
        public bool CanActivate => hasTarget && target.GetComponent<Producer>().IsActive && target.GetComponent<Producer>().AvailableCapacity >= rate;

        protected override bool FilterTargets(Collider2D collider)
        {
            var targetComponent = collider.GetComponentInParent<Producer>();
            return targetComponent != null && targetComponent.resourceType == resourceType && targetComponent.AvailableCapacity >= rate;
        }

        // TODO might need logic to break the link if the producer moves out of range (currently neither consumers nor producers can be moved,
        // but there is nothing preventing either from having a Container with activeWhileCarried attached to it, making it movable)

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
            }
        }
    }
}