using UnityEngine;


namespace AstroSurveyor
{
    public class Interact : Target
    {
        PlayerControls controller;

        void Start()
        {
            controller = GetComponent<PlayerControls>();
        }

        protected override bool FilterTargets(Collider2D collider)
        {
            if (controller.InHands != null)
            {
                var carriedComponent = controller.InHands.GetComponent<Interactive>();
                if (carriedComponent != null && carriedComponent.mustCarry)
                {
                    var carriedCollider = controller.InHands.GetComponent<Collider2D>();
                    return carriedCollider == collider;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                var targetComponent = collider.GetComponentInParent<Interactive>();
                return targetComponent != null && targetComponent.mustCarry == false && (targetComponent.type == InteractionType.ONCE && targetComponent.IsActive) == false;
            }
        }
    }
}