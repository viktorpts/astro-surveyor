using System.Linq;
using UnityEngine;


namespace AstroSurveyor
{
    public class Interact : Target
    {
        protected override bool FilterTargets(Collider2D collider) {
            var targetComponent = collider.GetComponentInParent<Interactive>();
            return targetComponent != null;
        }
    }
}