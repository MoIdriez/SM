using System;
using System.Drawing;
using Simulation.Helpers;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class Ray : SceneObject
    {
        public Point Start { get; private set; }
        public double Angle { get; private set; }
        public Point Direction { get { return GetEndPoint(10) - Start; } }

        public double X { get { return Start.X; } }
        public double Y { get { return Start.Y; } }

        public Pen Pen { get; set; }

        public Ray(Point start, double angle, Pen pen = null)
        {
            Start = start;
            Angle = angle < 0 ? angle + 365 : angle;
            Pen = pen ?? GetDefaultPen();
        }

        public Ray(Point start, Point onRay)
        {
            Start = start;
            Angle = start.GetAngleBetween(onRay);
        }

        public void Rotate(double r)
        {
            Angle += r;
        }

        public double GetSlope()
        {
            var endPoint = GetEndPoint(10);
            return endPoint.Y - Start.Y / endPoint.X - Start.X;
        }

        public Point GetEndPoint(double l)
        {
            return new Point(Start.X + l * Math.Cos(Angle.ToRad()), Start.Y + l * Math.Sin(Angle.ToRad()));
        }

        public void DrawWithEndPoint(Graphics g, Point end, Pen pen = null)
        {
            pen = pen ?? Pen;

            Start.Draw(g, pen);
            g.DrawLine(pen,
                (float)Start.X, (float)Start.Y,
                (float)end.X, (float)end.Y);
        }

        public void Draw(Graphics g, Pen pen = null)
        {
            pen = pen ?? Pen;
            Point end = GetEndPoint(5000);

            Start.Draw(g, pen);
            g.DrawLine(pen,
                (float)Start.X, (float)Start.Y,
                (float)end.X, (float)end.Y);
        }

        public Pen GetDefaultPen()
        {
            Pen rayPen = new Pen(Color.Black);
            rayPen.DashStyle = DashStyle.Dash;
            return rayPen;
        }
    }
}
