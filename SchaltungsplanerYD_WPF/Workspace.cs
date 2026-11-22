using SchaltungsplanerYD_WPF.Components;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Security.Cryptography.X509Certificates;

namespace SchaltungsplanerYD_WPF
{

    internal class Workspace
    {
        private Canvas canvas;
        private List<Component> components = new();
        public Point GridUnit { get; private set; }
        public Point SpaceBetweenUnits => new Point(GridUnit.X*2,GridUnit.Y*2);
        public Point GridSize = new Point(200, 100);


        //int newX = Math.Round(oldX / gridCubeWidth) * gridCubeWidth;
        //int newY = Math.Round(oldY / gridCubeHeight) * gridCubeHeight;

        public Workspace(Canvas icanvas)
        {
            canvas = icanvas;
            GridUnit = new Point(10, 10);

            for (int i = 0; i < GridSize.X; i++)
            {
                for (int j = 0; j < GridSize.Y; j++)
                {
                    Rectangle rect = new() 
                    { 
                        Fill = Brushes.Black, 
                        Width = GridUnit.X, Height = GridUnit.Y,
                        StrokeThickness = 0
                    };

                    Canvas.SetLeft(rect, i*SpaceBetweenUnits.X);
                    Canvas.SetTop(rect, j*SpaceBetweenUnits.Y);
                    canvas.Children.Add(rect);
                }
            }

            Add(new Wire(new Point(6, 5), this));
            Add(new Wire(new Point(7, 6), this));
            Add(new Wire(new Point(8, 7), this));
            Draw();
        }

        public void Add(Component c)
        {
            components.Add(c);
        }

        public void Draw()
        {
            for (int i = 0; i < components.Count; i++)
            {
                Rectangle rect = new()
                {
                    Fill = components[i].Color,
                    Width = components[i].Size.X, Height = components[i].Size.Y
                };

                Canvas.SetLeft(rect, SpaceBetweenUnits.X*components[i].Pos.X);
                Canvas.SetTop(rect, SpaceBetweenUnits.Y*components[i].Pos.Y);
                canvas.Children.Add(rect);
            }
        }
    }
}
