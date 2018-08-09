using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Objects
{
    public class Planner
    {
        public List<Step> Steps { get; private set; }

        public Planner(Point start, double startAngle, double endAngle)
        {
            var startStep = new Step(start, startAngle, endAngle);
            Steps = new List<Step> { startStep };
        }

        // adding step without changing rotation
        public void AddStep(Point p)
        {
            var step = new Step(p, Steps.Last().FOVStart.Angle, Steps.Last().FOVEnd.Angle);
            Steps.Add(step);
        }

        // adding step without changing rotation
        public void AddStep(double x, double y)
        {
            var step = new Step(new Point(x, y), Steps.Last().FOVStart.Angle, Steps.Last().FOVEnd.Angle);
            Steps.Add(step);
        }

        // adding step for rotation
        public void AddRotation(double r, int steps)
        {
            var start = Steps.Last();
            var rStep = r / steps;
            for (int i = 0; i <= steps; i++)
            {
                var startAngle = start.FOVStart.Angle + rStep * i;
                var endAngle = start.FOVEnd.Angle + rStep * i ;

                var step = new Step(new Point(start.Point.X, start.Point.Y), startAngle, endAngle);
                Steps.Add(step);
            }
        }

        // adding multiple steps without rotation from last point
        public void AddCourse(Point end, int steps)
        {
            var start = Steps.Last();
            double xStep = (end.X - start.Point.X) / steps;
            double yStep = (end.Y - start.Point.Y) / steps;

            for (int i = 0; i <= steps; i++)
            {
                var p = new Point(
                    start.Point.X + xStep * i,
                    start.Point.Y + yStep * i
                    );
                var step = new Step(p, start.FOVStart.Angle, start.FOVEnd.Angle);
                Steps.Add(step);
            }
        }

    }
}
