using System.Windows.Shapes;
using System.Windows;

namespace SchaltungsplanerYD_WPF.Components
{
    internal class Wire : UIElement
    {
        public Rectangle Rect;
        public Component ConnectedComp1, ConnectedComp2;
        public bool Selected;

        internal Wire(Rectangle iRect, Component iConnectedComp1, Component iConnectedComp2)
        {
            Rect = iRect;
            ConnectedComp1 = iConnectedComp1;
            ConnectedComp2 = iConnectedComp2;
        }
    }
}
