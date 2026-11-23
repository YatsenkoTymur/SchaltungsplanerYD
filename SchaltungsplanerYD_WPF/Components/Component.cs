using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SchaltungsplanerYD_WPF
{
    abstract class Component : UIElement
    {
        public Point Pos;
        public Point Size;
        public Brush Color;
        public Rectangle Rect;
        protected Workspace Workspace;

        public Component(Point iPos, Point iSize, Workspace iWorkspace, Brush iColor)
        {
            Pos = iPos;
            Size = iSize;
            Workspace = iWorkspace;
            Color = iColor;

            Rect = new()
            {
                Fill = Color,
                Width = Size.X, Height = Size.Y,
                Tag = this
            };
        }
        public Component() { }
    }
}
