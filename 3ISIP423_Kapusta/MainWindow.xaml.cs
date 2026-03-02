using System;
using System.ComponentModel;
using System.Windows;

namespace _3ISIP423_Kapusta
{
    public partial class MainWindow : Window
    {
        private Page1 page1;
        private Page2 page2;
        private Page3 page3;

        public MainWindow()
        {
            InitializeComponent(); // upd поправить

            // работает все ок
            page1 = new Page1();
            page2 = new Page2();
            page3 = new Page3();

            // ок
            MainFrame.Navigate(page1);
        }

        private void Page1_Checked(object sender, RoutedEventArgs e)
        {
            if (MainFrame != null && page1 != null) // Добавим проверку
                MainFrame.Navigate(page1);
        }

        private void Page2_Checked(object sender, RoutedEventArgs e)
        {
            if (MainFrame != null && page2 != null)
                MainFrame.Navigate(page2);
        }

        private void Page3_Checked(object sender, RoutedEventArgs e)
        {
            if (MainFrame != null && page3 != null)
                MainFrame.Navigate(page3);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Вы уверены, что хотите выйти из супер приложения?",
                "Подтверждение выхода",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}