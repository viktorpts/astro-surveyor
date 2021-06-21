using UnityEngine;


namespace AstroSurveyor
{
    public class Formation : Interactive
    {
        public GameObject specimenType;
        public bool scannerRequired;
        public bool harvested;

        public override bool MeetsRequirements => base.MeetsRequirements && scannerRequired == false;
    }
}