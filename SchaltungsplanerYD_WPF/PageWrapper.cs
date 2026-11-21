using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace SchaltungsplanerYD_WPF
{
    internal class PageWrapper
    {
        Page page;
        public object Content;
        //Add more properties as needed
        public PageWrapper(Page iPage)
        {
            page = iPage;
            Content = page.Content;
        }
    }
}
