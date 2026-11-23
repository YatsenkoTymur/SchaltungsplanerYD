using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace SchaltungsplanerYD_WPF
{
    internal class Toolbox
    {
        private StackPanel StackPanel;

        public Toolbox(StackPanel iStackPanel)
        {
            StackPanel = iStackPanel;


            StackPanel.Children.Add(new Button() { Content = "Resistor", Width = 100,Height=100, });
        }
    }
}
