namespace Calculator
{
    public static class CalculationHelper
    {
        public static float CalculateNeutrophilPercentage(float leik, float neitrof)
        {
            return MathF.Round((leik * neitrof) / 100, 2);
        }

        public static float CalculateLymphocytePercentage(float leik, float limfo)
        {
            return MathF.Round((leik * limfo) / 100, 2);
        }

        public static float CalculateMonocytePercentage(float leik, float monocit)
        {
            return MathF.Round((leik * monocit) / 100, 2);
        }

        public static float CalculateLCR(float limf10, float belok)
        {
            return MathF.Round((limf10 / belok) * 10000, 2);
        }

        public static float CalculateCLR(float belok, float limf10)
        {
            return MathF.Round(belok / limf10, 2);
        }

        public static float CalculateCAR(float belok, float albym)
        {
            return MathF.Round(belok / albym, 2);
        }

        public static float CalculateNLPR(float neitrofil, float limf, float trombocit)
        {
            return MathF.Round((neitrofil * 100) / (limf * trombocit), 2);
        }

        public static float CalculateNLR(float neitrofil10, float limf10)
        {
            return MathF.Round(neitrofil10 / limf10, 2);
        }

        public static float CalculateCally(float belok, float albym, float limf10)
        {
            return MathF.Round(((albym * limf10) * 100) / belok, 2);
        }

        public static float CalculateTIG(float albym, float limf10, float tromb, float belok, float neitrof10)
        {
            return MathF.Round((albym * limf10 * tromb) / (belok * neitrof10), 2);
        }

        public static float CalculateSIRI(float neitrof10, float monocit10, float limf10)
        {
            return MathF.Round((neitrof10 * monocit10) / limf10, 2);
        }

        public static float CalculatePNI(float albym, float limf10)
        {
            return MathF.Round(albym + (5 * limf10), 2);
        }

        public static float CalculateMII(float nlr, float belok)
        {
            return MathF.Round(nlr * belok, 2);
        }
    }
}
