using UnityEngine;

namespace AstroSurveyor
{
    public class Hint : MonoBehaviour
    {
        public GameObject tooltip;

        void Start()
        {
            tooltip.SetActive(false);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                tooltip.SetActive(true);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                tooltip.SetActive(false);
            }
        }
    }
}
