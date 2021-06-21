using System.Collections.Generic;
using UnityEngine;


namespace AstroSurveyor
{
    public class Producer : MonoBehaviour
    {
        // COnfig
        public ResourceType resourceType;
        [SerializeField]
        private int rate = 1;

        // State
        private bool isActive;
        public List<Consumer> consumers;
        private int consumption;

        // Query
        public int AvailableCapacity => rate - consumption;
        public bool IsActive => isActive;

        void Start()
        {
            consumers = new List<Consumer>();
        }

        public bool Link(Consumer consumer)
        {
            if (isActive && AvailableCapacity >= consumer.rate)
            {
                consumption += consumer.rate;
                consumers.Add(consumer);
                // TODO render connector between Producer and Consumer

                return true;
            }
            else
            {
                return false;
            }
        }

        public void UnLink(Consumer consumer)
        {
            if (consumers.Contains(consumer))
            {
                consumption -= consumer.rate;
                consumers.Remove(consumer);
                consumer.Deactivate();
            }
        }

        public void Activate()
        {
            // TODO animation
            isActive = true;
        }

        public void Deactivate()
        {
            // TODO animation
            foreach (var consumer in consumers)
            {
                UnLink(consumer);
            }
            isActive = false;
        }
    }
}