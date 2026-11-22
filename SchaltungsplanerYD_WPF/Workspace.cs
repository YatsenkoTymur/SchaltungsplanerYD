using SchaltungsplanerYD_WPF.Components;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Documents;

namespace SchaltungsplanerYD_WPF
{

    internal class Workspace
    {
        public enum TerminalState { None, Plus, Minus, NonPolar }

        private Canvas canvas;
        private TextBlock debugLabel;
        private List<Component> components = new();
        private double zoom;
        public Point GridUnit { get; private set; }
        public Point SpaceBetweenUnits => new Point(GridUnit.X*3,GridUnit.Y*3);
        public Point GridSize = new Point(100, 100);


        //int newX = Math.Round(oldX / gridCubeWidth) * gridCubeWidth;
        //int newY = Math.Round(oldY / gridCubeHeight) * gridCubeHeight;

        public Workspace(Canvas icanvas, TextBlock idebugLabel)
        {
            canvas = icanvas;
            debugLabel = idebugLabel;
            canvas.MouseWheel += Canvas_OnMouseWheel;

            GridUnit = new Point(30, 30);

            for (int i = 0; i < GridSize.X; i++)
            {
                for (int j = 0; j < GridSize.Y; j++)
                {
                    WorkspaceCell cell = new(i, j, this);

                    Canvas.SetLeft(cell.Rect, i*SpaceBetweenUnits.X);
                    Canvas.SetTop(cell.Rect, j*SpaceBetweenUnits.Y);
                    canvas.Children.Add(cell.Rect);
                }
            }

            Add(new Wire(new Point(6, 5), this));
            Add(new Wire(new Point(7, 6), this));
            Add(new Wire(new Point(8, 7), this));
        }

        public void Add(Component c)
        {
            components.Add(c);
            c.Rect.MouseLeftButtonDown += Rect_OnMouseLeftButtonDown;

            Canvas.SetLeft(c.Rect, SpaceBetweenUnits.X * c.Pos.X);
            Canvas.SetTop(c.Rect, SpaceBetweenUnits.Y * c.Pos.Y);
            canvas.Children.Add(c.Rect);
        }

        public void Rect_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((sender as Rectangle).Tag is Component c) canvas.Children.Remove((Rectangle)sender);
        }

        public void Canvas_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            zoom = GridUnit.X;
            if (e.Delta > 0 && GridUnit.X > 1) GridUnit = new Point(GridUnit.X -1, GridUnit.Y -1);
            else if (e.Delta < 0 && GridUnit.X < 60) GridUnit = new Point(GridUnit.X +1, GridUnit.Y +1);
            zoom = GridUnit.X / zoom;

            for (int i = 0; i < canvas.Children.Count; i++)
            {
                if (canvas.Children[i] is Rectangle rect)
                {
                    if (rect.Tag is WorkspaceCell cell)
                    {
                        Canvas.SetLeft(rect, cell.X * SpaceBetweenUnits.X);
                        Canvas.SetTop(rect, cell.Y * SpaceBetweenUnits.Y);
                    }
                    else if (rect.Tag is Component c)
                    {
                        Canvas.SetLeft(rect, c.Pos.X * SpaceBetweenUnits.X);
                        Canvas.SetTop(rect, c.Pos.Y * SpaceBetweenUnits.Y);
                    }

                    rect.Width *= zoom;
                    rect.Height *= zoom;

                    debugLabel.Text = zoom.ToString("0.00000") ;
                }
            }
        }
    }
}
