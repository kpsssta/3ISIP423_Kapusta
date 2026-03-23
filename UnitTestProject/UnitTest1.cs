using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Тестирование функции 1 из Page1
        /// w = |cos x - cos y|^(1+2sin²y) * (1 + z + z²/2 + z³/3 + z⁴/4)
        /// </summary>
        [TestMethod]
        public void TestFunction1_CalculateW()
        {
            // Тест 1: проверка при x=0, y=0, z=0
            double result1 = CalculateFunction1(0, 0, 0);
            Assert.AreEqual(0, result1, 0.000001, "При x=0,y=0,z=0 результат должен быть 0");

            // Тест 2: проверка при x=π/2, y=0, z=0
            double result2 = CalculateFunction1(Math.PI / 2, 0, 0);
            Assert.IsTrue(!double.IsNaN(result2), "Результат не должен быть NaN");

            // Тест 3: проверка при положительных значениях
            double result3 = CalculateFunction1(1, 2, 3);
            Assert.IsTrue(result3 > 0, "Результат должен быть положительным");
        }

        /// <summary>
        /// Тестирование функции 2 из Page2
        /// Кусочная функция с выбором f(x)
        /// </summary>
        [TestMethod]
        public void TestFunction2_CalculateD()
        {
            // Тест 1: случай x > y (x=10, y=5)
            double result1 = CalculateFunction2(10, 5, "x2");
            Assert.IsTrue(result1 > 0, "При x>y результат должен быть положительным");

            // Тест 2: случай x < y (x=1, y=10)
            double result2 = CalculateFunction2(1, 10, "exp");
            Assert.IsTrue(result2 > 0, "При x<y результат должен быть положительным");

            // Тест 3: случай x = y (x=5, y=5)
            double result3 = CalculateFunction2(5, 5, "sh");
            Assert.IsTrue(result3 > 0.5, "При x=y результат должен быть >0.5");
        }

        /// <summary>
        /// Тестирование функции 3 из Page3
        /// y = a·x³ + cos²(x³ - b)
        /// </summary>
        [TestMethod]
        public void TestFunction3_CalculateY()
        {
            // Тест 1: при x=0, a=1, b=0
            double result1 = CalculateFunction3(1, 0, 0);
            Assert.AreEqual(1, result1, 0.000001, "При x=0 результат должен быть 1");

            // Тест 2: при x=1, a=1, b=0
            double result2 = CalculateFunction3(1, 0, 1);
            double expected2 = 1 + Math.Pow(Math.Cos(1), 2);
            Assert.AreEqual(expected2, result2, 0.000001, "Ошибка при x=1");

            // Тест 3: при a=0, b=0, x=10
            double result3 = CalculateFunction3(0, 0, 10);
            double expected3 = Math.Pow(Math.Cos(Math.Pow(10, 3)), 2);
            Assert.AreEqual(expected3, result3, 0.000001, "Ошибка при a=0");
        }

        #region Вспомогательные методы

        private double CalculateFunction1(double x, double y, double z)
        {
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

        private double CalculateFunction2(double x, double y, string functionType)
        {
            double fx;

            switch (functionType)
            {
                case "sh":
                    fx = Math.Sinh(x);
                    break;
                case "x2":
                    fx = x * x;
                    break;
                case "exp":
                    fx = Math.Exp(x);
                    break;
                default:
                    fx = Math.Sinh(x);
                    break;
            }

            double result;

            if (x > y)
            {
                result = Math.Pow(fx - y, 3) + Math.Atan(fx);
            }
            else if (y > x)
            {
                result = Math.Pow(y - fx, 3) + Math.Atan(fx);
            }
            else
            {
                result = Math.Pow(y + fx, 3) + 0.5;
            }

            return result;
        }

        private double CalculateFunction3(double a, double b, double x)
        {
            double x3 = Math.Pow(x, 3);
            double cosArg = x3 - b;
            double cosVal = Math.Cos(cosArg);
            double cosSq = cosVal * cosVal;

            return a * x3 + cosSq;
        }

        #endregion
    }
}