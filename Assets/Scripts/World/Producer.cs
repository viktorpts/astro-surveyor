using System.Collections.Generic;
using UnityEngine;


namespace AstroSurveyor
{
    public class Producer : MonoBehaviour
    {
        // Config
        public ResourceType resourceType;
        public GameObject connectorPrefab;
        [SerializeField]
        private int rate = 1;

        // State
        private bool isActive;
        public List<Consumer> consumers;
        Dictionary<Consumer, GameObject> connectors;
        private int consumption;

        // Query
        public int AvailableCapacity => isActive ? rate - consumption : 0;
        public bool IsActive => isActive;

        void Start()
        {
            consumers = new List<Consumer>();
            connectors = new Dictionary<Consumer, GameObject>();
        }

        public bool Link(Consumer consumer)
        {
            if (isActive && AvailableCapacity >= consumer.rate)
            {
                consumption += consumer.rate;
                consumers.Add(consumer);

                var connector = Instantiate(connectorPrefab, transform.position, Quaternion.identity);

                var targetVector = consumer.transform.position - transform.position;
                connector.GetComponentInChildren<SpriteRenderer>().size = new Vector2(targetVector.magnitude, 0.1f);

                var rot = Quaternion.LookRotation(Vector3.forward, targetVector);
                connector.transform.rotation = rot;

                connectors.Add(consumer, connector);

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
                var connector = connectors[consumer];
                connectors.Remove(consumer);
                Destroy(connector);
            }
        }

        public void Activate()
        {
            // TODO animation
            isActive = true;
        }

        public void Deactivate()
        {
            var consumerArray = consumers.ToArray();
            // TODO animation
            foreach (var consumer in consumerArray)
            {
                UnLink(consumer);
            }
            isActive = false;
        }
    }
}