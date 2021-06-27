using System.Linq;
using UnityEngine;


namespace AstroSurveyor
{
    public class Demolish : Interactive
    {
        public float radius = 0.5f;
        public ToolType toolType = ToolType.EXPLOSIVE;

        public override bool MeetsRequirements()
        {
            var formation = FindFormation();
            if (formation == null)
            {
                GameManager.Instance.ShowMessage("This equipment cannot be used here");
                return false;
            }
            else
            {
                return base.MeetsRequirements();
            }
        }

        public override void Activate(bool isScanner)
        {
            var source = FindFormation();
            var formation = source.GetComponent<Formation>();

            base.Activate(isScanner);

            GetComponentInChildren<Animator>().SetBool("boom", true);
            Destroy(formation.gameObject);
            Invoke("Explode", 0.5f);
        }

        void Explode()
        {
            Destroy(gameObject);
        }

        GameObject FindFormation()
        {
            var center = transform.position;

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius)
                    .Where(o => o.GetComponent<Formation>() != null)
                    .Where(o => o.GetComponent<Formation>().toolRequired == toolType)
                    .OrderBy((o) => Vector3.SqrMagnitude(o.gameObject.transform.position - center))
                    .ToArray();

            var first = hitColliders.FirstOrDefault();
            if (first != null)
            {
                return first.gameObject;
            }
            return null;
        }
    }
}