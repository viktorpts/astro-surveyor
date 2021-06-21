using UnityEngine;


namespace AstroSurveyor
{
    public class Deploy : Interactive
    {
        public GameObject result;

        public override void Activate(bool isScanner)
        {
            base.Activate(isScanner);
            var sourcePos = gameObject.transform.position;
            Instantiate(result, sourcePos, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}