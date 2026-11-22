using System.Windows;
using System.Windows.Media;

namespace SchaltungsplanerYD_WPF
{
    abstract class Component
    {
        public Point Pos;
        public Point Size;
        public Brush Color;
        protected Workspace Workspace;

        public Component(Point iPos, Point iSize, Workspace iWorkspace, Brush iColor)
        {
            Pos = iPos;
            Size = iSize;
            Workspace = iWorkspace;
            Color = iColor;
        }

        //abstract public void Draw();
    }
}
