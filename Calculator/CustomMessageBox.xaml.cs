using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox(string message, string patientName)
        {
            InitializeComponent();
            MessageText.Text = message + patientName;
        }
        private void CloseWindw_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrintInfo_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            // Проверяем, выбран ли принтер
            if (printDialog.ShowDialog() == true)
            {
                // Печатаем текущий элемент, например, TextBlock с сообщением
                // Можно напечатать любой элемент управления, например, весь UI или только его часть
                printDialog.PrintVisual(MessageText, "Сообщение для печати");
            }

            this.Close();
        }
    }
}
