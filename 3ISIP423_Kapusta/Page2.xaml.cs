using System;
using System.Windows;
using System.Windows.Controls;

namespace _3ISIP423_Kapusta
{
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void btnCalculate2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заполненности
                if (string.IsNullOrWhiteSpace(txtX2.Text) ||
                    string.IsNullOrWhiteSpace(txtY2.Text))
                {
                    MessageBox.Show("Где-то не заполнили поля!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Парсинг
                if (!double.TryParse(txtX2.Text.Replace('.', ','), out double x))
                {
                    MessageBox.Show("Некорректное значение x!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(txtY2.Text.Replace('.', ','), out double y))
                {
                    MessageBox.Show("Некорректное значение y!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Вычисляем f(x) в зависимости от выбора
                double fx;
                if (rbtnSh.IsChecked == true)
                {
                    fx = Math.Sinh(x);
                }
                else if (rbtnX2.IsChecked == true)
                {
                    fx = x * x;
                }
                else
                {
                    fx = Math.Exp(x);
                }

                // Вычисляем d по условию
                double result;

                if (x > y)
                {
                    result = Math.Pow(fx - y, 3) + Math.Atan(fx);
                }
                else if (y > x)
                {
                    result = Math.Pow(y - fx, 3) + Math.Atan(fx);
                }
                else // x == y
                {
                    result = Math.Pow(y + fx, 3) + 0.5;
                }

                // Вывод
                txtResult2.Text = result.ToString("F10");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClear2_Click(object sender, RoutedEventArgs e)
        {
            txtX2.Clear();
            txtY2.Clear();
            txtResult2.Clear();
            rbtnSh.IsChecked = true;
        }
    }
}