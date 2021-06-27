using UnityEngine;


namespace AstroSurveyor
{
    public class SimpleFormation : Interactive
    {
        public GameObject specimenType;
        public bool harvested;

        public override void Activate(bool isScanner)
        {
            if (harvested)
            {
                GameManager.Instance.ShowMessage("This formation has already been sampled!");
                return;
            }
            base.Activate(isScanner);
            var center = transform.position;
            var result = specimenType;
            var specimen = Instantiate(result, center, Quaternion.identity);

            var offset = (Vector3) Random.insideUnitCircle.normalized * 1.25f;
            specimen.GetComponent<Container>().AnimateSpawn(center + offset);

            harvested = true;
            GameManager.Instance.ShowMessage("Specimen extracted");
        }
    }
}