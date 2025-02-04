using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        public static int COUNT = 0;
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

                1. ФИО:
                    Вводится раздельно с заглавной буквы 'Иванов Иван Ванович'

                2. Процент нейтрофилов:
                   Формула: (Лейкоциты * Нейтрофилы) / 100

                3. Процент лимфоцитов:
                   Формула: (Лейкоциты * Лимфоциты) / 100

                4. Процент моноцитов:
                   Формула: (Лейкоциты * Моноциты) / 100

                5. LCR:
                   Формула: (Лимфоциты / Белок) * 10000

                6. CLR:
                   Формула: Белок / Лимфоциты

                7. CAR:
                   Формула: Белок / Альбумин

                8. NLPR:
                   Формула: (Нейтрофилы * 100) / (Лимфоциты * Тромбоциты)
 
                9. NLR:
                   Формула: Нейтрофилы / Лимфоциты

                10. Cally:
                   Формула: ((Альбумин * Лимфоциты) * 100) / Белок

                11. TIG:
                    Формула: (Альбумин * Лимфоциты * Тромбоциты) / (Белок * Нейтрофилы)

                12. SIRI:
                    Формула: (Нейтрофилы * Моноциты) / Лимфоциты

                13. PNI:
                    Формула: Альбумин + (0,005 * Лимфоциты * 1000)

                14. MII:
                    Формула: NLR * Белок
                ");
        }


        private void Button_click(object sender, RoutedEventArgs e)
        {
            if (TryInputFio(fio, out string fioResult, "ФИО пациента") ||
                TryParseInput(reakBelok, out float belok, "реак. белок") ||
                TryParseInput(leikochit, out float leik, "Лейкоциты") ||
                TryParseInput(albymin, out float albym, "Альбумин") ||
                TryParseInput(neyrofil, out float neyr, "Нейтрофилы") ||
                TryParseInput(limfo, out float lim, "Лимфоциты") ||
                TryParseInput(monocit, out float monoc, "Моноциты") ||
                TryParseInput(trombocit, out float tromb, "Тромбоциты")
                )
            {
                return;
            }

            float resultneitr = CalculationHelper.CalculateNeutrophilPercentage(leik, neyr);
            float resultlimf = CalculationHelper.CalculateLymphocytePercentage(leik, lim); 
            float resultmonoc = CalculationHelper.CalculateMonocytePercentage(leik, monoc); 
            float resultLCR = CalculationHelper.CalculateLCR(resultlimf, belok); 
            float resultCLR = CalculationHelper.CalculateCLR(belok, resultlimf); 
            float resultCAR = CalculationHelper.CalculateCAR(belok, albym); 
            float resultNLPR = CalculationHelper.CalculateNLPR(resultneitr, resultlimf, tromb); 
            float resultNLR = CalculationHelper.CalculateNLR(resultneitr, resultlimf); 
            float resultCally = CalculationHelper.CalculateCally(belok, albym, resultlimf); 
            float resultTIG = CalculationHelper.CalculateTIG(albym, resultlimf, tromb, belok, resultneitr); 
            float resultSIRI = CalculationHelper.CalculateSIRI(resultneitr, resultmonoc, resultlimf); 
            float resultPNI = CalculationHelper.CalculatePNI(albym, resultlimf);
            float resultMII = CalculationHelper.CalculateMII(resultNLR, belok); 

            SetResult(resultNeirtofil, resultneitr);
            SetResult(resultLimfocit, resultlimf);
            SetResult(resultMonocit, resultmonoc);
            SetResult(resultLCRed, resultLCR, 120, (v, c) => v <= c);
            SetResult(resultCLRed, resultCLR, 77.7f, (v,c) => v >= c);
            SetResult(resultCARed, resultCAR, 2.51f, (v, c) => v >= c);
            SetResult(resultNLPRed, resultNLPR, 1.83f, (v, c) => v >= c);
            SetResult(resultNLRed, resultNLR, 3.8f, (v, c) => v >= c);
            SetResult(resultCallyed, resultCally, 47, (v, c) => v <= c);
            SetResult(resultTIGed, resultTIG, 12.8f, (v, c) => v <= c);
            SetResult(resultSIRIed, resultSIRI, 3.06f, (v, c) => v >= c);
            SetResult(resultPNIed, resultPNI, 37, (v, c) => v <= c);
            SetResult(resultMIIed, resultMII, 334, (v, c) => v >= c);

            PrintInfo printInfo = new PrintInfo(
                fio.Text,
                resultneitr,
                resultlimf,
                resultmonoc,
                resultLCR,
                resultCLR,
                resultCAR,
                resultNLPR,
                resultNLR,
                resultCally,
                resultTIG,
                resultSIRI,
                resultPNI,
                resultMII,
                DateTime.Now
                );

            ShowMessageBox(oritCheckBox.IsChecked, printInfo);
            COUNT = 0;
        }

        private void ShowMessageBox(bool? checkBoxStatus, PrintInfo printInfo)
        {
            string message = string.Empty;

            if (checkBoxStatus == true && COUNT >= 6)
                message = "Состояние пациента критическое с неблагоприятным прогнозом.";
            else if (checkBoxStatus == false && COUNT >= 6)
                message = "Состояние пациента критическое.";
            else if (COUNT <= 4)
                message = "Состояние пациента неудовлетворительное";
            else if (COUNT == 0)
                message = "Состояние пациента удовлетворительное.";

            CustomMessageBox customMessageBox = new CustomMessageBox(message, printInfo);
            customMessageBox.ShowDialog();
        }

        private bool TryInputFio(TextBox inputBox, out string result, string fieldName)
        {
            result = inputBox.Text.Trim();

            return !InputValidation.IsValidFio(result, fieldName);
        }


        private bool TryParseInput(TextBox inputBox, out float result, string fieldName)
        {
            return !InputValidation.TryParseFloat(inputBox.Text, out result, fieldName);
        }

        private void SetResult(TextBox resultBlock, float value)
        {
            resultBlock.Text = value.ToString(CultureInfo.InvariantCulture);
        }

        private void SetResult(TextBox resultBlock, float value, float compareDigit, Func<float, float, bool> compare)
        {
            resultBlock.Text = value.ToString(CultureInfo.InvariantCulture);

            if (compare(value, compareDigit))
            {
                resultBlock.Foreground = Brushes.Red;
                resultBlock.TextDecorations = TextDecorations.Underline;
                COUNT++;
            }
            else
            {
                resultBlock.Foreground = new SolidColorBrush(Color.FromRgb(0x56, 0xb8, 0x14));
                resultBlock.TextDecorations = null;
            }
        }
    }
}
