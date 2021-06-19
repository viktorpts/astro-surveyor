using System.Linq;
using UnityEngine;


namespace AstroSurveyor
{
    public class Carry : MonoBehaviour
    {
        public bool hasTarget = false;
        public GameObject target = null;
        public RectTransform arrow;
        public float radius = 0.5f;

        void Start()
        {
            var arrowObject = GameObject.FindWithTag("TargetPointer");
            arrow = arrowObject.GetComponent<RectTransform>();
        }

        void Update()
        {
            var center = transform.position;

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius)
                    .Where(o => o.gameObject.tag == "Container")
                    .Where(o => o.GetComponentInParent<Container>().IsCarried == false)
                    .OrderBy((o) => Vector3.SqrMagnitude(o.gameObject.transform.position - center))
                    .ToArray();

            var first = hitColliders.FirstOrDefault();
            if (first != null)
            {
                hasTarget = true;
                target = first.gameObject;
                var offsetX = Camera.main.WorldToScreenPoint(first.transform.position).x;
                var offsetY = Camera.main.WorldToScreenPoint(first.transform.position).y;
                arrow.anchoredPosition = new Vector2(offsetX, offsetY + 50);
                arrow.gameObject.SetActive(true);
            }
            else
            {
                hasTarget = false;
                target = null;
                arrow.gameObject.SetActive(false);
            }
        }
    }
}
