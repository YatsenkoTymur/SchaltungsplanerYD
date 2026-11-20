using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace SchaltungsplanerYD_WPF
{
    static internal class SharedState
    {
        static internal Dictionary<string, Page> Pages = new Dictionary<string, Page> {
            { "MainPage",null },
            { "TestPage",null }
        };
    }
}
