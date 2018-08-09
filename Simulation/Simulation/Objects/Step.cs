using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Objects
{
    public class Step
    {
        public Point Point { get; set; }
        public Ray FOVStart { get; private set; }
        public Ray FOVEnd { get; private set; }

        public Step(Step step)
        {
            Point = step.Point;
            FOVStart = step.FOVStart;
            FOVEnd = step.FOVEnd;
        }

        public Step(Point p, double startAngle, double endAngle)
        {
            Point = p;
            FOVStart = new Ray(Point, startAngle);
            FOVEnd = new Ray(Point, endAngle);
        }

        public Step(double x, double y, double startAngle, double endAngle)
        {
            Point = new Point(x, y);
            FOVStart = new Ray(Point, startAngle);
            FOVEnd = new Ray(Point, endAngle);
        }

        public void Rotate(double r)
        {
            FOVStart.Rotate(r);
            FOVEnd.Rotate(r);
        }
    }
}
