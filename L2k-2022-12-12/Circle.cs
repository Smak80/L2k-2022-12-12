using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2k_2022_12_12
{
    public class Circle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }
        public int R { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Circle(Color color, int r, Size containerSize)
        {
            Color = color;
            R = r;
            Width = containerSize.Width;
            Height = containerSize.Height;
        }

        public void Move(int dx, int dy)
        {
            X += dx;
            X = X.CoerceIn(0, Width - 2 * R);
            Y += dy;
            Y = Y.CoerceIn(0, Height - 2 * R);
        }

        public void Paint(Graphics g)
        {
            var p = new SolidBrush(Color);
            g.FillEllipse(p, new Rectangle(X, Y, 2*R, 2*R));
        }
    }
}

static class Coercer
{
    public static int CoerceIn(this int value, double min, double max) =>
        (int)Math.Min(max, Math.Max(min, value));
}
