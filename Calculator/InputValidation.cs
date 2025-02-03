using System.Text.RegularExpressions;
using System.Windows;

namespace Calculator
{
    public class InputValidation
    {
        public static bool IsValidFio(string input, string fieldName)
        {
            string pattern = @"^[А-ЯЁ][а-яё]+ [А-ЯЁ][а-яё]+ [А-ЯЁ][а-яё]+$";
            Regex regex = new Regex(pattern);

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show($"Поле '{fieldName}' не может быть пустым.");
                return false;
            }

            if (!regex.IsMatch(input))
            {
                MessageBox.Show($"Введите корректное ФИО в поле '{fieldName}'.");
                return false;
            }

            return true;
        }

        public static bool TryParseFloat(string input, out float result, string fieldName)
        {
            result = 0;
            if (string.IsNullOrEmpty(input) || !float.TryParse(input, out result))
            {
                MessageBox.Show($"Некорректное значение в поле '{fieldName}'");
                return false;
            }
            return true;
        }
    }
}
