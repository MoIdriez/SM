using System;
using System.Collections.Generic;
using System.Drawing;
using Engine;
using Simulation.Helpers;
using Simulation.Objects;

namespace Simulation
{
    public static class ExpSimpleCircles
    {
        public static void Run(Graphics g)
        {
            Engine.Point cameraLocation = new Engine.Point(800, 300);
            Camera camera = new Camera(new Step(cameraLocation, 315, 45, 0));

            List<SceneObject> cs = new List<SceneObject> {
                    new Circle(new Engine.Point(800, 500), 20),
                    new Circle(new Engine.Point(800, 700), 20),
                    new Circle(new Engine.Point(1000, 500), 20),
                    new Circle(new Engine.Point(700, 200), 20)
            };


            Scene scene = new Scene(camera, g, cs);

            scene.Draw();

            var fov = camera.GetFOVRays();
            fov.ForEach(vp => {
                vp.Draw(g, Pens.LightBlue);
                cs.ForEach(c =>
                {
                    var pts = vp.RayCircleIntersections((Circle)c);
                    if (pts.Item1 == 1) { pts.Item2.DrawWithRadius(g, 2, Pens.Green); }
                    if (pts.Item1 == 2)
                    {
                        var it2St = pts.Item2.DistanceTo(vp.Start) < pts.Item3.DistanceTo(vp.Start);
                        pts.Item2.DrawWithRadius(g, 2, it2St ? Pens.Green : Pens.Red);
                        pts.Item3.DrawWithRadius(g, 2, it2St ? Pens.Red : Pens.Green);
                    }
                });                
            });

            
            
        }
    }
}
