namespace Calculator
{
    public class PrintInfo
    {
        public readonly string PatientName;
        public readonly float Neitrofil;
        public readonly float Limfocit;
        public readonly float Monict;
        public readonly float LCRed;
        public readonly float CLRed;
        public readonly float CARed;
        public readonly float NLPRed;
        public readonly float NLRed;
        public readonly float Cally;
        public readonly float TIG;
        public readonly float SIRI;
        public readonly float PNI;
        public readonly float MII;
        public readonly DateTime Date;

        public PrintInfo(string patientName, float neitrofil, float limfocit, float monict, float lCRed, float cLRed, float cARed, float nLPRed, float nLRed, float cally, float tIG, float sIRI, float pNI, float mII, DateTime date)
        {
            PatientName = patientName;
            Neitrofil = neitrofil;
            Limfocit = limfocit;
            Monict = monict;
            LCRed = lCRed;
            CLRed = cLRed;
            CARed = cARed;
            NLPRed = nLPRed;
            NLRed = nLRed;
            Cally = cally;
            TIG = tIG;
            SIRI = sIRI;
            PNI = pNI;
            MII = mII;
            Date = date;
        }
    }
}
