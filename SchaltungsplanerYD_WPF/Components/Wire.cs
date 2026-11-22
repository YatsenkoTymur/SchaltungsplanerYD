using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SchaltungsplanerYD_WPF.Components
{
    class Wire : Component
    {
        public Wire(Point iPos, Workspace iWorkspace) :
            base(iPos, iWorkspace.GridUnit, iWorkspace, Brushes.Silver)
        { }
    }
}
