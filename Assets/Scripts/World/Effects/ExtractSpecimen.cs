using System.Linq;
using UnityEngine;


namespace AstroSurveyor
{
    public class ExtractSpecimen : Interactive
    {
        public float radius = 0.5f;

        public override bool MeetsRequirements
        {
            get
            {
                var formation = FindFormation();
                return formation != null && formation.GetComponent<Formation>().IsActive;
            }
        }

        public override void Activate()
        {
            var center = transform.position;
            var result = FindFormation().GetComponent<Formation>().specimenType;
            Instantiate(result, center, Quaternion.identity);
        }

        public override void Deactivate() { }

        GameObject FindFormation()
        {
            var center = transform.position;

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius)
                    .Where(o => o.GetComponent<Formation>() != null)
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