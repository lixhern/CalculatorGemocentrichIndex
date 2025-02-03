using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    public partial class CustomMessageBox : Window
    {
        private PrintInfo printInfo;
        public CustomMessageBox(string message, PrintInfo printInfo)
        {
            InitializeComponent();

            this.printInfo = printInfo;

            MessageText.Text = message;
        }
        private void CloseWindw_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrintInfo_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            string pdfPath = $"{printInfo.PatientName}.pdf";
            GeneratorPDF.GeneratePDF(pdfPath, printInfo);

/*            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = pdfPath,   // Путь к PDF файлу
                Verb = "print",           // печать
                CreateNoWindow = true
            };

            // Запуск процесса печати 
            Process.Start(startInfo);*/

            this.Close();
        }
    }
}
