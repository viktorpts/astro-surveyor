using UnityEngine;


namespace AstroSurveyor
{
    public class Deploy : Interactive
    {
        public GameObject result;

        public override void Activate()
        {
            base.Activate();
            var sourcePos = gameObject.transform.position;
            Instantiate(result, sourcePos, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}