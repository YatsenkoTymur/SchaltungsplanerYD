using SchaltungsplanerYD_WPF.Components;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SchaltungsplanerYD_WPF
{

    internal class Workspace
    {
        public enum TerminalState { None, Plus, Minus, NonPolar }

        public Point GridUnit { get; private set; } = new Point(30, 30);
        public Point SpaceBetweenUnits { get { return new Point(GridUnit.X * 3, GridUnit.Y * 3); } }
        public Point GridSize = new Point(100, 100);
        private Canvas canvas;
        private TextBlock debugLabel;
        private Cursor cursor = new Cursor();
        private List<Component> components = new();
        private List<Wire> wires = new();
        private Brush[] wireBrushes = new Brush[] { Brushes.Red, Brushes.Orange, Brushes.Yellow,
            Brushes.GreenYellow, Brushes.Green, Brushes.DarkGreen, Brushes.LightBlue, 
            Brushes.Blue, Brushes.DarkBlue, Brushes.Indigo, Brushes.Violet };
        private double zoom = 1;


        //int newX = Math.Round(oldX / gridCubeWidth) * gridCubeWidth;
        //int newY = Math.Round(oldY / gridCubeHeight) * gridCubeHeight;

        public Workspace(Canvas icanvas, TextBlock idebugLabel)
        {
            canvas = icanvas;
            debugLabel = idebugLabel;
            canvas.MouseDown += Canvas_OnMouseDown;
            canvas.MouseWheel += Canvas_OnMouseWheel;
            canvas.KeyDown += Canvas_OnKeyDown;

            for (int i = 0; i < GridSize.X; i++)
            {
                for (int j = 0; j < GridSize.Y; j++)
                {
                    WorkspaceCell cell = new(i, j, this);

                    Canvas.SetLeft(cell.Rect, i*SpaceBetweenUnits.X);
                    Canvas.SetTop(cell.Rect, j*SpaceBetweenUnits.Y);
                    canvas.Children.Add(cell.Rect);
                    cell.Rect.MouseRightButtonDown += Rect_OnMouseRightButtonDown;
                }
            }
        }

        public void Add(Component c)
        {
            components.Add(c);
            c.Rect.MouseLeftButtonDown += Rect_OnMouseLeftButtonDown;
            //c.Rect.MouseRightButtonDown += Rect_OnMouseRightButtonDown;

            Canvas.SetLeft(c.Rect, SpaceBetweenUnits.X * c.Pos.X);
            Canvas.SetTop(c.Rect, SpaceBetweenUnits.Y * c.Pos.Y);
            canvas.Children.Add(c.Rect);
        }

        public void Connect(List<Component> cArr)
        {
            if (cArr.Count < 2) return;
            for (int i = 1; i < cArr.Count; i++)
            {
                bool alreadyConnected = false;
                for (int j = 0; j < wires.Count; j++)
                {
                    if ((wires[j].ConnectedComp1.Pos == cArr[i - 1].Pos && wires[j].ConnectedComp2.Pos == cArr[i].Pos) ||
                        (wires[j].ConnectedComp1.Pos == cArr[i].Pos && wires[j].ConnectedComp2.Pos == cArr[i - 1].Pos))
                    {
                        alreadyConnected = true;
                        break;
                    }
                }
                if (alreadyConnected) continue;

                if (cArr[i - 1].Pos == cArr[i].Pos) continue;
                else if (cArr[i - 1].Pos.X != cArr[i].Pos.X &&
                    cArr[i - 1].Pos.Y != cArr[i].Pos.Y)
                {
                    Point middlePoint = new Point(cArr[i].Pos.X, cArr[i - 1].Pos.Y);
                    Solder middleSolder = null;
                    bool alreadyPlaced = false;
                    for (int j = 0; j < components.Count; j++)
                    {
                        if (components[j].Pos==middlePoint && components[j] is Solder)
                        {
                            middleSolder = (Solder)components[j];
                            alreadyPlaced = true;
                            break;
                        }
                    }
                    if (!alreadyPlaced)
                    {
                        middleSolder = new Solder(middlePoint, this);
                        Add(middleSolder);
                    }
                    Connect(new List<Component> { cArr[i - 1], middleSolder });
                    Connect(new List<Component> { middleSolder, cArr[i] });
                }
                else
                {
                    Point rectPos, rectSize;
                    if (cArr[i - 1].Pos.X == cArr[i].Pos.X)
                    {
                        double rectVecY = cArr[i].Pos.Y - cArr[i - 1].Pos.Y;
                        double lesserPointY = (cArr[i - 1].Pos.Y < cArr[i].Pos.Y) ?
                            cArr[i - 1].Pos.Y : cArr[i].Pos.Y;

                        rectPos = new Point(
                            (cArr[i - 1].Pos.X * SpaceBetweenUnits.X) + (GridUnit.X / 4),
                            (SpaceBetweenUnits.Y * lesserPointY) + (GridUnit.Y / 2));
                        rectSize = new Point(
                            GridUnit.X / 2,
                            Math.Abs(rectVecY) * SpaceBetweenUnits.Y);
                    }
                    else // T: (cArr[i - 1].Pos.Y == cArr[i].Pos.Y)
                    {
                        double rectVecX = cArr[i].Pos.X - cArr[i - 1].Pos.X;
                        double lesserPointX = (cArr[i - 1].Pos.X < cArr[i].Pos.X) ?
                            cArr[i - 1].Pos.X : cArr[i].Pos.X;

                        rectPos = new Point(
                            (SpaceBetweenUnits.X * lesserPointX) + (GridUnit.X / 2),
                            (cArr[i - 1].Pos.Y * SpaceBetweenUnits.Y) + (GridUnit.Y / 4));
                        rectSize = new Point(
                            Math.Abs(rectVecX) * SpaceBetweenUnits.X,
                            GridUnit.Y / 2);
                    }

                    Wire wire = new Wire(new Rectangle()
                    {
                        Width = rectSize.X,
                        Height = rectSize.Y,
                        Fill = wireBrushes[wires.Count % wireBrushes.Length]
                    }, cArr[i - 1], cArr[i]);
                    wire.Rect.Tag = wire;

                    canvas.Children.Add(wire.Rect);
                    wires.Add(wire);

                    Canvas.SetLeft(wire.Rect, rectPos.X);
                    Canvas.SetTop(wire.Rect, rectPos.Y);
                }
            }
        }

        public void Rect_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((sender as Rectangle).Tag is Component c)
            {
                if (c.Selected) cursor.Unselect(c);
                else cursor.Select(c);
            }
        }

        public void Rect_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((sender as Rectangle).Tag is WorkspaceCell cell) Add(new Solder(new Point(cell.X, cell.Y), this));
            else //((sender as Rectangle).Tag is Component c)
                 canvas.Children.Remove((UIElement)sender);
        }

        public void Canvas_OnMouseDown(object sender, MouseEventArgs e)
        {
            Keyboard.Focus(canvas);
        }

        public void Canvas_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            zoom = GridUnit.X;
            Point oldSpaceBetweenUnits = SpaceBetweenUnits;

            if (e.Delta > 0 && GridUnit.X > 1) GridUnit = new Point(GridUnit.X -1, GridUnit.Y -1);
            else if (e.Delta < 0 && GridUnit.X < 60) GridUnit = new Point(GridUnit.X +1, GridUnit.Y +1);
            zoom = GridUnit.X / zoom;

            for (int i = 0; i < canvas.Children.Count; i++)
            {
                if (canvas.Children[i] is Rectangle rect)
                {
                    Canvas.SetLeft(rect, (Canvas.GetLeft(rect) / oldSpaceBetweenUnits.X) * SpaceBetweenUnits.X);
                    Canvas.SetTop(rect, (Canvas.GetTop(rect) / oldSpaceBetweenUnits.Y) * SpaceBetweenUnits.Y);

                    rect.Width *= zoom;
                    rect.Height *= zoom;

                    debugLabel.Text = zoom.ToString("0.0000000") ;
                }
            }
        }

        public void Canvas_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C)
            {
                Connect(cursor.Selected);
                cursor.ClearSelected();
            }
        }
    }
}
