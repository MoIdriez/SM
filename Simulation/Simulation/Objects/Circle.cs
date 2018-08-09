using System.Drawing;

namespace Engine
{
    public class Circle : SceneObject
    {
        public Point Center { get; private set; }
        public double Radius { get; private set; }
        public Pen Pen { get; set; }

        public Circle(Point center, double radius, Pen pen = null)
        {
            Center = center;
            Radius = radius;
            Pen = pen ?? GetDefaultPen();
        }

        public Circle(double centerX, double centerY, double radius)
        {
            Center = new Point(centerX, centerY);
            Radius = radius;
        }

        public void Draw(Graphics g, Pen p = null)
        {
            p = p ?? Pen;
            Simulation.Draw.DrawCircle(g, p, this);
        }

        public Pen GetDefaultPen()
        {
            return Pens.Blue;
        }
    }
}
