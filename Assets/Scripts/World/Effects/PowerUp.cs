using UnityEngine;


namespace AstroSurveyor
{
    public class PowerUp : Interactive
    {
        public override void Activate()
        {
            base.Activate();
            gameObject.transform.GetComponentInChildren<SpriteRenderer>().color = new Color(0.5f, 1f, 0.75f);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            gameObject.transform.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        }
    }
}