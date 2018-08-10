using System;
using System.Drawing;

namespace Engine
{
    public class Point : SceneObject
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public Pen Pen { get; set; }
        public bool IsDrawn { get; set; }

        public Point(double x, double y, Pen pen = null)
        {
            X = x;
            Y = y;
            Pen = pen ?? GetDefaultPen();
            IsDrawn = true;
        }

        public void DrawWithRadius(Graphics g, double r, Pen p = null)
        {
            if (!IsDrawn) return;
            p = p ?? Pen;
            Simulation.Draw.DrawCircle(g, p, (float)X, (float)Y, (float)r);
        }

        public void Draw(Graphics g, Pen p = null)
        {
            if (!IsDrawn) return;
            p = p ?? Pen;
            Simulation.Draw.DrawCircle(g, p, (float)X, (float)Y, 20);
        }

        public Pen GetDefaultPen()
        {
            return Pens.Blue;
        }

        public Point Normalized()
        {
            var length = Math.Sqrt(X*X + Y*Y);
            return new Point(X/length, Y/length);
        }

        public double DistanceTo(Point p)
        {
            return Math.Sqrt(Math.Pow(p.X-X, 2) + Math.Pow(p.Y-Y, 2));
        }

        public static Point operator -(Point p1, double number)
        {
            return new Point(p1.X - number, p1.Y - number);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point operator +(Point p1, double number)
        {
            return new Point(p1.X + number, p1.Y + number);
        }

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static double operator *(Point p1, Point p2)
        {
            return p1.X*p2.X+p1.Y*p2.Y;
        }

        public static Point operator *(Point p, double number)
        {
            return new Point(p.X*number, p.Y*number);
        }

        public static Point operator /(Point p, double number)
        {
            return new Point(p.X / number, p.Y / number);
        }
    }
}
