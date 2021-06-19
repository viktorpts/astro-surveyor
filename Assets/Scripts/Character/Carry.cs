using System.Linq;
using UnityEngine;


namespace AstroSurveyor
{
    public class Carry : Target
    {
        protected override bool FilterTargets(Collider2D collider) {
            var targetComponent = collider.GetComponentInParent<Container>();
            return targetComponent != null && targetComponent.IsCarried == false;
        }
    }
}
