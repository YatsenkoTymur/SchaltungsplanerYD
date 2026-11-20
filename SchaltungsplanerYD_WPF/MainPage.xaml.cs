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
    /// Interaktionslogik für MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            SharedState.Pages["MainPage"] = this;
        }

        private void Button1_OnClick(object sender, RoutedEventArgs e)
        {
            Page testPage = SharedState.Pages["TestPage"] != null 
                ? SharedState.Pages["TestPage"] : new TestPage();
            NavigationService.Navigate(testPage);
        }

        private void Button2_OnClick(object sender, RoutedEventArgs e)
        {
        }
    }
}
