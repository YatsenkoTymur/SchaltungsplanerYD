using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace SchaltungsplanerYD_WPF
{
    internal class WorkspaceCell : UIElement
    {
        public Workspace.TerminalState Terminal;
        public int X;
        public int Y;
        public Rectangle Rect;

        public WorkspaceCell(int iX, int iY, Workspace iworkspace)
        {
            X = iX; Y = iY;
            Rect = new()
            {
                Fill = Brushes.Black,
                Width = iworkspace.GridUnit.X, Height = iworkspace.GridUnit.Y,
                Tag = this
            };
        }
    }
}
