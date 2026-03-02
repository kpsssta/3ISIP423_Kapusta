using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace _3ISIP423_Kapusta
{
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();

            // пустая модель
            var model = new PlotModel();
            plotView.Model = model;
        }

        private void btnCalculate3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // парсинг
                if (!double.TryParse(txtA.Text.Replace('.', ','), out double a) ||
                    !double.TryParse(txtB.Text.Replace('.', ','), out double b) ||
                    !double.TryParse(txtX0.Text.Replace('.', ','), out double x0) ||
                    !double.TryParse(txtXk.Text.Replace('.', ','), out double xk) ||
                    !double.TryParse(txtDx.Text.Replace('.', ','), out double dx))
                {
                    MessageBox.Show("Некорректные значения!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка направления шага
                if (xk > x0 && dx <= 0)
                {
                    MessageBox.Show("При xₖ > x₀ шаг dx должен быть положительным!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (xk < x0 && dx >= 0)
                {
                    MessageBox.Show("При xₖ < x₀ шаг dx должен быть отрицательным!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Вычисление
                string resultText = "     x          y\n";
                resultText += "------------------\n";

                var points = new List<DataPoint>();

                double x = x0;
                int count = 0;

                while ((dx > 0 && x <= xk + 0.000001) || (dx < 0 && x >= xk - 0.000001))
                {
                    // тут формула
                    double x3 = Math.Pow(x, 3);           // x³
                    double cosArg = x3 - b;                // x³ - b
                    double cosVal = Math.Cos(cosArg);      // cos(x³ - b)
                    double cosSq = cosVal * cosVal;        // cos²(x³ - b)
                    double y = a * x3 + cosSq;             // a*x³ + cos²(x³ - b)

                    points.Add(new DataPoint(x, y));

                    resultText += $"{x,8:F4}  {y,10:F4}\n";

                    x += dx;
                    count++;

                    if (count > 1000)
                    {
                        MessageBox.Show("Слишком много точек! Уменьшите диапазон.",
                            "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    }
                }

                // Выводим результаты
                txtResults.Text = resultText;

                // Строим график
                CreateChart(points);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateChart(List<DataPoint> points)
        {
            var model = new PlotModel { Title = "График функции y = a·x³ + cos²(x³ - b)" };

            // Настраиваем оси
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "x",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColors.LightGray
            };

            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "y",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColors.LightGray
            };

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            // линия графика
            var series = new LineSeries
            {
                Title = "y = a·x³ + cos²(x³ - b)",
                Color = OxyColors.Blue,
                StrokeThickness = 2,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                MarkerFill = OxyColors.Red,
                LineStyle = LineStyle.Solid
            };

            // Добавляем точки
            foreach (var point in points)
            {
                series.Points.Add(point);
            }

            model.Series.Add(series);

            // Автоматически подбираем диапазоны осей
            model.ResetAllAxes();

            // Обновляем график
            plotView.Model = model;
            plotView.InvalidatePlot(true);
        }

        private void btnClear3_Click(object sender, RoutedEventArgs e)
        {
            txtA.Text = "1";
            txtB.Text = "0";
            txtX0.Text = "-2";
            txtXk.Text = "2";
            txtDx.Text = "0.1";
            txtResults.Clear();

            // Очищаем график
            plotView.Model = new PlotModel();
        }
    }
}