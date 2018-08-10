using Engine;
using Simulation.Helpers;
using Simulation.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Simulation.Examples
{
    public static class ExpLineIntersection
    {
        public static void Run(Graphics g)
        {
            Engine.Point cameraLocation = new Engine.Point(400, 400);
            Camera camera = new Camera(new Step(cameraLocation, 315, 45, 0));

            var arena = new Polygon(new List<LineSegment>
            {
                new LineSegment(new Engine.Point(600, 100), new Engine.Point(1000,100)),
                new LineSegment(new Engine.Point(1000, 100), new Engine.Point(1000,300)),
                new LineSegment(new Engine.Point(1000, 300), new Engine.Point(1400,300)),
                new LineSegment(new Engine.Point(1400, 300), new Engine.Point(1400,0)),
                new LineSegment(new Engine.Point(1400, 0), new Engine.Point(1800,0)),
                new LineSegment(new Engine.Point(1800, 0), new Engine.Point(1800, 800)),
                new LineSegment(new Engine.Point(1800, 800), new Engine.Point(1400, 800)),
                new LineSegment(new Engine.Point(1400, 800), new Engine.Point(1400, 500)),
                new LineSegment(new Engine.Point(1400, 500), new Engine.Point(1000, 500)),
                new LineSegment(new Engine.Point(1000, 500), new Engine.Point(1000, 700)),
                new LineSegment(new Engine.Point(1000, 700), new Engine.Point(600, 700))
            });

            Scene scene = new Scene(camera, g, new List<SceneObject> { arena });

            scene.Draw();

            var fov = camera.GetFOVRays();
            fov.ForEach(vp =>
            {
                

                var pts = arena.Lines.Select(l => vp.RayLineSegmentIntersection(l)).Where(p => p != null);

                if (pts.Count() >= 1)
                {
                    var pt = pts.Select(p => new Tuple<Engine.Point, double>(p, p.DistanceTo(camera.Location))).OrderBy(p => p.Item2).First();
                    pt.Item1.DrawWithRadius(g, 5, Pens.Green);
                    vp.DrawWithEndPoint(g, pt.Item1, Pens.LightBlue);
                } else
                {
                    vp.Draw(g, Pens.LightBlue);
                }
            });
        }
    }
}
