using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SchaltungsplanerYD_WPF.Components
{
    internal class Resistor : Component
    {
        public double Resistance;
        public double Multiplier;
        public double Tolerance;
        public int BandCount;
        public int TemperatureCoefficient;

        //The getter calculates the colors dynamically
        public List<Brush> Bands
        {
            get
            {
                List<Brush> result = new();
                Dictionary<int, Brush> SignificantFigures = new() { { 0, Brushes.Black }, { 1, Brushes.Brown }, { 2, Brushes.Red }, { 3, Brushes.Orange }, { 4, Brushes.Yellow }, { 5, Brushes.Green }, { 6, Brushes.Blue }, { 7, Brushes.Violet }, { 8, Brushes.Gray }, { 9, Brushes.White } };
                Dictionary<double, Brush> Multipliers = new() { { 1, Brushes.Black }, { 10, Brushes.Brown }, { 100, Brushes.Red }, { 1000, Brushes.Orange }, { 10000, Brushes.Yellow }, { 100000, Brushes.Green }, { 1 * 10 ^ 6, Brushes.Blue }, { 1 * 10 ^ 7, Brushes.Violet }, { 1 * 10 ^ 8, Brushes.Gray }, { 1 * 10 ^ 9, Brushes.White }, { 0.1, Brushes.Gold }, { 0.01, Brushes.Silver } };
                if (BandCount == 0 || BandCount == 3)
                {
                    var dig1 = GetDigit((int)Resistance, 0);
                    var dig2 = GetDigit((int)Resistance, 1);
                    Multiplier = Resistance / (dig1 * 10 + dig2);
                    result.AddRange([SignificantFigures[dig1], SignificantFigures[dig2], Multipliers[Multiplier]]);
                    BandCount = 3;
                }

                return result;
            }
            set;
        }
        public Resistor(Point iPos, Point iSize, Workspace iWorkspace, Brush iColor,double iResistance) : base(iPos, iSize, iWorkspace, iColor)
        {
            Resistance = iResistance;
            Console.WriteLine(Bands);
        }
        public Resistor(double iResistance)
        {
            Resistance = iResistance;
            Console.WriteLine(Bands);
        }
    

//Move this to an utils class?
static int GetDigit(int number, int k)
        {
            var temp1= number.ToString().Reverse();
            var temp2 = temp1.ToArray();
            string str = new(temp2);
            number = Convert.ToInt32(str);
            return (int)(number / Math.Pow(10, k)) % 10;
        }
    }

}
