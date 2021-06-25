using System.Linq;
using UnityEngine;


namespace AstroSurveyor
{
    public class ExtractSpecimen : Interactive
    {
        public float radius = 0.5f;
        public ToolType toolType = ToolType.SAMPLER;

        public override bool MeetsRequirements()
        {
            var formation = FindFormation();
            if (formation == null)
            {
                GameManager.Instance.ShowMessage("This tool cannot be used here");
                return false;
            }
            else if (base.MeetsRequirements())
            {
                var component = formation.GetComponent<Formation>();
                if (component.IsActive == false)
                {
                    GameManager.Instance.ShowMessage("Equipment not calibrated: Examine the formation first");
                    return false;
                }
                else if (component.harvested)
                {
                    GameManager.Instance.ShowMessage("This formation has already been sampled!");
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Activate(bool isScanner)
        {
            var formation = FindFormation().GetComponent<Formation>();
            if (formation.harvested)
            {
                GameManager.Instance.ShowMessage("This formation has already been sampled!");
                return;
            }
            base.Activate(isScanner);
            var center = transform.position;
            var result = formation.specimenType;
            Instantiate(result, center, Quaternion.identity);
            formation.harvested = true;
            GameManager.Instance.ShowMessage("Specimen extracted");
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