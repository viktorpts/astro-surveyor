using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace AstroSurveyor
{
    public class PickUpZone : MonoBehaviour
    {
        GameObject zone;
        bool inZone = false;
        bool engaging = false;
        float timeEngaged = 0f;
        [SerializeField]
        List<GameObject> specimens;


        void Start()
        {
            specimens = new List<GameObject>();
            zone = transform.GetChild(2).gameObject;
        }

        void Update()
        {
            GameManager.Instance.UpdateTooltip(zone.transform.position, timeEngaged);

            if (engaging && inZone)
            {
                timeEngaged += Time.deltaTime;
                if (timeEngaged >= 1f)
                {
                    // TODO show level summary overlay
                    GameManager.Instance.ShowMessage($"Exit triggered. Final score: {CalcScore()}");
                    engaging = false;
                }
            }
            else
            {
                if (timeEngaged > 0)
                {
                    timeEngaged = 0;
                }
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                inZone = true;
                GameManager.Instance.ShowTooltip(true);
            }
            else if (other.gameObject.CompareTag("Specimen"))
            {
                specimens.Add(other.gameObject);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                inZone = false;
                GameManager.Instance.ShowTooltip(false);
            }
            else if (other.gameObject.CompareTag("Specimen"))
            {
                specimens.Remove(other.gameObject);
            }
        }

        public void Engage(bool value)
        {
            engaging = value;
        }

        int CalcScore() {
            return specimens.Sum(s => s.GetComponent<Specimen>().points);
        }
    }
}