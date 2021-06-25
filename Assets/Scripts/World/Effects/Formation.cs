using UnityEngine;


namespace AstroSurveyor
{
    public class Formation : Interactive
    {
        public GameObject specimenType;
        public bool scannerRequired;
        public bool harvested;
        public ToolType toolRequired = ToolType.SAMPLER;

        public override bool MeetsRequirements()
        {
            if (scannerRequired)
            {
                GameManager.Instance.ShowMessage("This formation requires a Scanner");
                return false;
            }
            else
            {
                return base.MeetsRequirements();
            }
        }
    }
}