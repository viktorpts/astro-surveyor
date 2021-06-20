using System.Linq;
using UnityEngine;


namespace AstroSurveyor
{
    public abstract class Target : MonoBehaviour
    {
        public bool hasTarget = false;
        public GameObject target = null;
        public float radius = 0.5f;

        protected abstract bool FilterTargets(Collider2D collider);

        void Update()
        {
            var center = transform.position;

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius)
                    .Where(FilterTargets)
                    .OrderBy((o) => Vector3.SqrMagnitude(o.gameObject.transform.position - center))
                    .ToArray();

            var first = hitColliders.FirstOrDefault();
            if (first != null)
            {
                hasTarget = true;
                target = first.gameObject;
                // var offsetX = Camera.main.WorldToScreenPoint(first.transform.position).x;
                // var offsetY = Camera.main.WorldToScreenPoint(first.transform.position).y;
                // arrow.anchoredPosition = new Vector2(offsetX, offsetY + 50);
                // arrow.gameObject.SetActive(true);
            }
            else
            {
                hasTarget = false;
                target = null;
                // arrow.gameObject.SetActive(false);
            }
        }
    }
}
