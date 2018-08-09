using Engine;
using System.Drawing;

namespace Simulation
{
    public static class Draw
    {
        public static void DrawCircle(this Graphics g, Pen pen, Circle c)
        {
            g.DrawEllipse(pen, (float) (c.Center.X - c.Radius), (float) (c.Center.Y - c.Radius), (float) (c.Radius + c.Radius), (float) (c.Radius + c.Radius));
        }

        public static void DrawCircle(this Graphics g, Pen pen, float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius, radius + radius, radius + radius);
        }

        public static void FillCircle(this Graphics g, Brush brush,
                                      float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }
    }
}
