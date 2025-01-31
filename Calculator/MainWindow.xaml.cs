using System.Globalization;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void QuestionButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"Инструкция по вводу данных:

                - Вводите значения с десятичными дробями через запятую (например, 12,5).
                - Все параметры должны быть положительными числами.
                - Для расчета различных процентов и коэффициентов использовались следующие формулы:

                1. Процент нейтрофилов:
                   Формула: (Лейкоциты * Нейтрофилы) / 100

                2. Процент лимфоцитов:
                   Формула: (Лейкоциты * Лимфоциты) / 100

                3. Процент моноцитов:
                   Формула: (Лейкоциты * Моноциты) / 100

                4. LCR:
                   Формула: (Лимфоциты / Белок) * 10000

                5. CLR:
                   Формула: Белок / Лимфоциты

                6. CAR:
                   Формула: Белок / Альбумин

                7. NLPR:
                   Формула: (Нейтрофилы * 100) / (Лимфоциты * Тромбоциты)
 
                8. NLR:
                   Формула: Нейтрофилы / Лимфоциты

                9. Cally:
                   Формула: ((Альбумин * Лимфоциты) * 100) / Белок

                10. TIG:
                    Формула: (Альбумин * Лимфоциты * Тромбоциты) / (Белок * Нейтрофилы)

                11. SIRI:
                    Формула: (Нейтрофилы * Моноциты) / Лимфоциты

                12. PNI:
                    Формула: Альбумин + (0,005 * Лимфоциты * 1000)

                13. MII:
                    Формула: NLR * Белок
                ");
        }


        private void Button_click(object sender, RoutedEventArgs e)
        {
            if (!TryParseInput(reakBelok, out float belok, "реак. белок") ||
                !TryParseInput(leikochit, out float leik, "Лейкоциты") ||
                !TryParseInput(albymin, out float albym, "Альбумин") ||
                !TryParseInput(neyrofil, out float neyr, "Нейтрофилы") ||
                !TryParseInput(limfo, out float lim, "Лимфоциты") ||
                !TryParseInput(monocit, out float monoc, "Моноциты") ||
                !TryParseInput(trombocit, out float tromb, "Тромбоциты"))
            {
                return;
            }

            float resultneitr = CalculateNeutrophilPercentage(leik, neyr);
            float resultlimf = CalculateLymphocytePercentage(leik, lim);
            float resultmonoc = CalculateMonocytePercentage(leik, monoc);
            float resultLCR = CalculateLCR(resultlimf, belok);
            float resultCLR = CalculateCLR(belok, resultlimf);
            float resultCAR = CalculateCAR(belok, albym);
            float resultNLPR = CalculateNLPR(resultneitr, resultlimf, tromb);
            float resultNLR = CalculateNLR(resultneitr, resultlimf);
            float resultCally = CalculateCally(belok, albym, resultlimf);
            float resultTIG = CalculateTIG(albym, resultlimf, tromb, belok, resultneitr);
            float resultSIRI = CalculateSIRI(resultneitr, resultmonoc, resultlimf);
            float resultPNI = CalculatePNI(albym, resultlimf);
            float resultMII = CalculateMII(resultNLR, belok);

            SetResult(resultNeirtofil, resultneitr);
            SetResult(resultLimfocit, resultlimf);
            SetResult(resultMonocit, resultmonoc);
            SetResultReduction(resultLCRed, resultLCR, 120);
            SetResultEncrease(resultCLRed, resultCLR, 77.7f);
            SetResultEncrease(resultCARed, resultCAR, 2.51f);
            SetResultEncrease(resultNLPRed, resultNLPR, 1.83f);
            SetResultEncrease(resultNLRed, resultNLR, 3.8f);
            SetResultReduction(resultCallyed, resultCally, 47);
            SetResultReduction(resultTIGed, resultTIG, 12.8f);
            SetResultEncrease(resultSIRIed, resultSIRI, 3.06f);
            SetResultReduction(resultPNIed, resultPNI, 37);
            SetResultEncrease(resultMIIed, resultMII, 334);
        }

        private bool TryParseInput(TextBox inputBox, out float result, string fieldName)
        {
            result = 0;
            if (string.IsNullOrEmpty(inputBox.Text) || !float.TryParse(inputBox.Text, out result))
            {
                MessageBox.Show($"Некорректное значение в поле '{fieldName}'");
                return false;
            }
            return true;
        }

        private void SetResult(TextBox resultBlock, float value)
        {
            resultBlock.Text = value.ToString(CultureInfo.InvariantCulture);
        }

        private void SetResultEncrease(TextBox resultBlock, float value, float compareDigit)
        {
            resultBlock.Text = value.ToString(CultureInfo.InvariantCulture);
            resultBlock.Foreground = value >= compareDigit ? Brushes.Red : new SolidColorBrush(Color.FromRgb(0x56, 0xb8, 0x14));
        }

        private void SetResultReduction(TextBox resultBlock, float value, float compareDigit)
        {
            resultBlock.Text = value.ToString(CultureInfo.InvariantCulture);
            resultBlock.Foreground = value <= compareDigit ? Brushes.Red : new SolidColorBrush(Color.FromRgb(0x56, 0xb8, 0x14));
        }

        private float CalculateNeutrophilPercentage(float leik, float neitrof)
        {
            return (leik * neitrof) / 100;
        }

        private float CalculateLymphocytePercentage(float leik, float limfo)
        {
            return (leik * limfo) / 100;
        }

        private float CalculateMonocytePercentage(float leik, float monocit)
        {
            return (leik * monocit) / 100;
        }

        private float CalculateLCR(float limf10, float belok)
        {
            return (limf10 / belok) * 10000;
        }

        private float CalculateCLR(float belok, float limf10)
        {
            return belok / limf10;
        }

        private float CalculateCAR(float belok, float albym)
        {
            return belok / albym;
        }

        private float CalculateNLPR(float neitrofil, float limf, float trombocit)
        {
            return (neitrofil * 100) / (limf * trombocit);
        }

        private float CalculateNLR(float neitrofil10, float limf10)
        {
            return neitrofil10 / limf10;
        }

        private float CalculateCally(float belok, float albym, float limf10)
        {
            return ((albym * limf10) * 100) / belok;
        }

        private float CalculateTIG(float albym, float limf10, float tromb, float belok, float neitrof10)
        {
            return (albym * limf10 * tromb) / (belok * neitrof10);
        }

        private float CalculateSIRI(float neitrof10, float monocit10, float limf10)
        {
            return (neitrof10 * monocit10) / limf10;
        }

        private float CalculatePNI(float albym, float limf10)
        {
            return albym + (5 * limf10);
        }

        private float CalculateMII(float nlr, float belok)
        {
            return nlr * belok;
        }
    }
}
