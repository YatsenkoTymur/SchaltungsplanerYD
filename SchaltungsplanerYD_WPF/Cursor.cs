using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace SchaltungsplanerYD_WPF
{
    internal class Cursor
    {
        public enum Tool { Select, Move, Place }

        public Point Pos = new Point(0,0);
        public List<Component> Selected = new List<Component>();
        public Tool CurrTool;
        public Component.Type CurrComponent;

        public Cursor()
        {
        
        }

        public void Select(Component c)
        {
            Selected.Add(c);
            //c.Rect.OpacityMask = new SolidColorBrush(Color.FromArgb(63, 0, 0, 255));
            c.Selected = true;
            c.Rect.Fill = Brushes.LightBlue;
        }

        public void Unselect(Component c)
        {
            Selected.Remove(c);
            //c.Rect.OpacityMask = new SolidColorBrush(Color.FromArgb(63, 0, 0, 255));
            c.Selected = false;
            c.Rect.Fill = c.Color;
        }

        public void ClearSelected()
        {
            while (Selected.Count > 0) Unselect(Selected[0]);
        }
    }
}
