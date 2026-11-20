using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchaltungsplanerYD_WPF
{
    /// <summary>
    /// Interaktionslogik für TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        int a = 0;
        public TestPage()
        {
            InitializeComponent();
            SharedState.Pages["TestPage"] = this;
        }

        private void TestPage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            a++;
            TestLabel.Content = a;
            if (a>20)
            {
                NavigationService.Navigate(SharedState.Pages["MainPage"]);
            }
        }
    }
}
