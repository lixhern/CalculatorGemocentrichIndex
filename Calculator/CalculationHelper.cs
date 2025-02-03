namespace Calculator
{
    public static class CalculationHelper
    {
        public static float CalculateNeutrophilPercentage(float leik, float neitrof)
        {
            return (leik * neitrof) / 100;
        }

        public static float CalculateLymphocytePercentage(float leik, float limfo)
        {
            return (leik * limfo) / 100;
        }

        public static float CalculateMonocytePercentage(float leik, float monocit)
        {
            return (leik * monocit) / 100;
        }

        public static float CalculateLCR(float limf10, float belok)
        {
            return (limf10 / belok) * 10000;
        }

        public static float CalculateCLR(float belok, float limf10)
        {
            return belok / limf10;
        }

        public static float CalculateCAR(float belok, float albym)
        {
            return belok / albym;
        }

        public static float CalculateNLPR(float neitrofil, float limf, float trombocit)
        {
            return (neitrofil * 100) / (limf * trombocit);
        }

        public static float CalculateNLR(float neitrofil10, float limf10)
        {
            return neitrofil10 / limf10;
        }

        public static float CalculateCally(float belok, float albym, float limf10)
        {
            return ((albym * limf10) * 100) / belok;
        }

        public static float CalculateTIG(float albym, float limf10, float tromb, float belok, float neitrof10)
        {
            return (albym * limf10 * tromb) / (belok * neitrof10);
        }

        public static float CalculateSIRI(float neitrof10, float monocit10, float limf10)
        {
            return (neitrof10 * monocit10) / limf10;
        }

        public static float CalculatePNI(float albym, float limf10)
        {
            return albym + (5 * limf10);
        }

        public static float CalculateMII(float nlr, float belok)
        {
            return nlr * belok;
        }
    }
}
