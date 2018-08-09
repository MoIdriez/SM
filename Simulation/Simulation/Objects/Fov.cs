using Engine;
using System;
using System.Drawing;

namespace Simulation.Objects
{
    public class Fov
    {
        public Fov(Ray ray, SceneObject intersectingObj, Engine.Point intersectingPoint)
        {
            Ray = ray;
            IntersectingObj = intersectingObj;
            IntersectingPoint = intersectingPoint;
        }
        public Fov(Ray ray, Tuple<SceneObject, Engine.Point> intersect)
        {
            Ray = ray;
            IntersectingObj = intersect.Item1;
            IntersectingPoint = intersect.Item2;
        }

        public void Draw(Graphics g)
        {
            if (IntersectingPoint != null)
            {
                IntersectingPoint.DrawWithRadius(g, 2, Pens.Green);
                Ray.DrawWithEndPoint(g, IntersectingPoint, Pens.LightBlue);
            } else
            {
                Ray.Draw(g, Pens.LightBlue);
            }

        }

        // main objects
        public Ray Ray { get; private set; }
        public SceneObject IntersectingObj { get; private set; }
        public Engine.Point IntersectingPoint { get; private set; }

        //convenience functions
        public double Distance { get { return IntersectingPoint == null ? -1 : Ray.Start.DistanceTo(IntersectingPoint); } }
        public Color Color { get { return IntersectingObj == null ? Pens.White.Color : IntersectingObj.Pen.Color; } }
    }
}
