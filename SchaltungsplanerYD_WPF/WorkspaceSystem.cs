using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Shapes;

namespace SchaltungsplanerYD_WPF
{
    
    internal class WorkspaceSystem
    {
        PageWrapper page;
        int gridWidth = 100;
        int gridHeight = 100;
        public object Content;

        //int newX = Math.Round(oldX / gridCubeWidth) * gridCubeWidth;
        //int newY = Math.Round(oldY / gridCubeHeight) * gridCubeHeight;
        
        public WorkspaceSystem(PageWrapper iPage)
        {
            page = iPage;
            Canvas canvas = new();
            for (int i = 0; i < gridWidth; i++)
                for (int j = 0; j < gridHeight; j++)
                {
                    var rect = new Rectangle() { Fill = Brushes.Black, Width = 5, Height = 5, StrokeThickness = 1 };
                    Canvas.SetTop(rect, j*gridHeight);
                    Canvas.SetLeft(rect, i*gridWidth);
                    canvas.Children.Add(rect);
                }
            Content = canvas;
            
            
            
        }

    }
}
