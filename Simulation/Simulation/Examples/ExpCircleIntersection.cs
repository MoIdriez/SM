using System;
using System.Collections.Generic;
using System.Drawing;
using Engine;
using Simulation.Helpers;

namespace Simulation
{
    public static class ExpCircleIntersection
    {

        public static void Run(Graphics g)
        {
            Ray r = new Ray(new Engine.Point(350, 350), 225);
            Circle c = new Circle(new Engine.Point(200, 200), 100);

            var pts = r.RayCircleIntersections(c);

            var it2St = pts.Item2.DistanceTo(r.Start) < pts.Item3.DistanceTo(r.Start);
            pts.Item2.Draw(g, it2St ? Pens.Green : Pens.Red);
            pts.Item3.Draw(g, it2St ? Pens.Red : Pens.Green);


            r.Draw(g);
            c.Draw(g);

        }
    }
}
