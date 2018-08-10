using Engine;
using Simulation.Helpers;
using Simulation.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Simulation.Examples
{
    public static class ExpLineAndCircleIntersection
    {
        public static void Run(Graphics g, int i) {
            Engine.Point cameraLocation = new Engine.Point(400 + i * 50, 400);
            Camera camera = new Camera(new Step(cameraLocation, 315, 45, 0));

            var arena = new Polygon(new List<LineSegment>
            {
                new LineSegment(new Engine.Point(600, 100), new Engine.Point(1000,100), Pens.Green),
                new LineSegment(new Engine.Point(1000, 100), new Engine.Point(1000,300)),
                new LineSegment(new Engine.Point(1000, 300), new Engine.Point(1400,300), Pens.Orange),
                new LineSegment(new Engine.Point(1400, 300), new Engine.Point(1400,0)),
                new LineSegment(new Engine.Point(1400, 0), new Engine.Point(1800,0), Pens.Red),
                new LineSegment(new Engine.Point(1800, 0), new Engine.Point(1800, 800), Pens.Red),
                new LineSegment(new Engine.Point(1800, 800), new Engine.Point(1400, 800), Pens.Red),
                new LineSegment(new Engine.Point(1400, 800), new Engine.Point(1400, 500)),
                new LineSegment(new Engine.Point(1400, 500), new Engine.Point(1000, 500), Pens.Orange),
                new LineSegment(new Engine.Point(1000, 500), new Engine.Point(1000, 700)),
                new LineSegment(new Engine.Point(1000, 700), new Engine.Point(600, 700), Pens.Green)
            });

            var circle = new Circle(new Engine.Point(800, 600), 20);
            var sceneObjects = new List<SceneObject> { arena, circle };
            Scene scene = new Scene(camera, g, sceneObjects);

            scene.Draw();
        }
    }
}
