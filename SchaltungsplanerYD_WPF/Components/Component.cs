using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SchaltungsplanerYD_WPF
{
    abstract class Component : UIElement
    {
        public enum Type { Component, Solder }

        public Point Pos;
        public Point Size;
        public Brush Color;
        public Rectangle Rect;
        public bool Selected;
        protected Workspace Workspace;

        protected Component(Point iPos, Point iSize, Workspace iWorkspace, Brush iColor)
        {
            Workspace = iWorkspace; // T: not used yet
            Color = iColor;
            Pos = iPos;
            Size = iSize;

            Rect = new()
            {
                Fill = Color,
                Width = Size.X, Height = Size.Y,
                Tag = this
            };
        }
    }
}
