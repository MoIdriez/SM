using System.Drawing;

namespace Engine
{
    public class LineSegment : SceneObject
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }
        public Pen Pen { get; set; }
        public bool IsDrawn { get; set; }

        public LineSegment(Point start, Point end, Pen pen = null)
        {
            Start = start;
            End = end;
            Pen = pen ?? GetDefaultPen();
            IsDrawn = true;
        } 

        public LineSegment(double startX, double startY, double endX, double endY)
        {
            Start = new Point(startX, startY);
            End = new Point(endX, endY);
            IsDrawn = true;
        }

        public double GetSlope()
        {
            return End.Y - Start.Y / End.X - Start.X;
        }

        public void Draw(Graphics g, Pen pen = null)
        {
            if (!IsDrawn) return;
            pen = pen ?? Pen;

            g.DrawLine(pen,
                (float)Start.X, (float)Start.Y,
                (float)End.X, (float)End.Y);
        }

        public Pen GetDefaultPen()
        {
            return new Pen(Color.Black);
        }
    }
}
