using System;
using System.Windows;
using System.Windows.Controls;

namespace _3ISIP423_Kapusta
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void btnCalculate1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заполненности полей
                if (string.IsNullOrWhiteSpace(txtX.Text) ||
                    string.IsNullOrWhiteSpace(txtY.Text) ||
                    string.IsNullOrWhiteSpace(txtZ.Text))
                {
                    MessageBox.Show("Заполните все поля!!!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Парсинг значений с проверкой
                if (!double.TryParse(txtX.Text.Replace('.', ','), out double x))
                {
                    MessageBox.Show("Некорректное значение x!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(txtY.Text.Replace('.', ','), out double y))
                {
                    MessageBox.Show("Некорректное значение y!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(txtZ.Text.Replace('.', ','), out double z))
                {
                    MessageBox.Show("Некорректное значение z!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Вычисление функции
                double result = CalculateFunction(x, y, z);

                // Вывод результата
                txtResult1.Text = result.ToString("F10");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при вычислении: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private double CalculateFunction(double x, double y, double z)
        {
            // w = |cos x - cos y|^(1+2sin²y) * (1 + z + z²/2 + z³/3 + z⁴/4)

            double cosX = Math.Cos(x);
            double cosY = Math.Cos(y);
            double diff = Math.Abs(cosX - cosY);

            double sinY = Math.Sin(y);
            double sinSq = sinY * sinY;
            double exponent = 1 + 2 * sinSq;

            double firstPart = Math.Pow(diff, exponent);

            double secondPart = 1 + z + (z * z) / 2 + (z * z * z) / 3 + (z * z * z * z) / 4;

            return firstPart * secondPart;
        }

        private void btnClear1_Click(object sender, RoutedEventArgs e)
        {
            txtX.Clear();
            txtY.Clear();
            txtZ.Clear();
            txtResult1.Clear();
        }
    }
}