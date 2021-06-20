using UnityEngine;


namespace AstroSurveyor
{
    public class Deploy : Interactive
    {
        public GameObject result;

        public override void Activate()
        {
            var sourcePos = gameObject.transform.position;
            Instantiate(result, sourcePos, Quaternion.identity);
            Destroy(gameObject);
        }

        public override void Deactivate() { }
    }
}