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
                if (base.MeetsRequirements && formation != null)
                {
                    var component = formation.GetComponent<Formation>();
                    return component.IsActive && component.harvested == false;
                }
                else
                {
                    return false;
                }
            }
        }

        public override void Activate()
        {
            base.Activate();
            var center = transform.position;
            var formation = FindFormation().GetComponent<Formation>();
            var result = formation.specimenType;
            Instantiate(result, center, Quaternion.identity);
            formation.harvested = true;
        }

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