using System.Linq;
using UnityEngine;


namespace AstroSurveyor
{
    public class Scanner : Interactive
    {
        public float radius = 5f;

        public override void Activate(bool isScanner)
        {
            base.Activate(isScanner);
            var center = transform.position;
            var formations = FindFormations();
            foreach(var formation in formations) {
                formation.Activate(true);
            }
        }

        Formation[] FindFormations()
        {
            var center = transform.position;

            Formation[] formations = Physics2D.OverlapCircleAll(center, radius)
                    .Select(o => o.GetComponent<Formation>())
                    .Where(o => o != null && o.IsActive == false)
                    .ToArray();

            return formations;
        }
    }
}