using Engine;
using Simulation.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Examples
{
    public static class ExpSimpleSlam
    {
        public static Scene Run(Graphics g, Step step)
        {
            Camera camera = new Camera(step);

            var arena = new Polygon(new List<LineSegment>
            {
                //outer lines
                new LineSegment(new Engine.Point(750, 400), new Engine.Point(600, 400)),
                new LineSegment(new Engine.Point(600, 400), new Engine.Point(600, 100)),
                new LineSegment(new Engine.Point(600, 100), new Engine.Point(1650, 100)),
                new LineSegment(new Engine.Point(1650, 100), new Engine.Point(1650, 850)),
                new LineSegment(new Engine.Point(1650, 850), new Engine.Point(600, 850)),
                new LineSegment(new Engine.Point(600, 850), new Engine.Point(600, 550)),
                new LineSegment(new Engine.Point(600, 550), new Engine.Point(750, 550)),
                //inner lines to create rooms
                new LineSegment(new Engine.Point(900, 100), new Engine.Point(900, 400)),
                new LineSegment(new Engine.Point(1050, 100), new Engine.Point(1050, 400)),
                new LineSegment(new Engine.Point(1200, 100), new Engine.Point(1200, 400)),

                new LineSegment(new Engine.Point(900, 550), new Engine.Point(900, 850)),
                new LineSegment(new Engine.Point(1050, 550), new Engine.Point(1050, 700)),
                new LineSegment(new Engine.Point(900, 700), new Engine.Point(1050, 700)),
                new LineSegment(new Engine.Point(1200, 550), new Engine.Point(1200, 850)),

                new LineSegment(new Engine.Point(1350, 550), new Engine.Point(1650, 550))
            });

            arena.Lines.ForEach(l => l.IsDrawn = false);

            var sceneObjects = new List<SceneObject> { arena };
            Scene scene = new Scene(camera, g, sceneObjects);

            scene.Draw();
            return scene;
        }
    }
}
