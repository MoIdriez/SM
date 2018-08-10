using Engine;

namespace Simulation.Objects
{
    public class Step
    {
        public Point Point { get; private set; }
        public double StartAngle { get; private set; }
        public double EndAngle { get; private set; }
        public double Noise { get; private set; }

        public Ray FOVStart {
            get {
                return new Ray(Point + Noise, StartAngle);
            }
        }
        
        public Ray FOVEnd
        {
            get
            {
                return new Ray(Point + Noise, EndAngle);
            }
        }

        public Step(Step step)
        {
            Point = step.Point;
            StartAngle = step.StartAngle;
            EndAngle = step.EndAngle;
            Noise = step.Noise;
        }

        public Step(Point p, double startAngle, double endAngle, double noise)
        {
            Point = p;
            StartAngle = startAngle;
            EndAngle = endAngle;
            Noise = noise;
        }

        public Step(double x, double y, double startAngle, double endAngle, double noise)
        {
            Point = new Point(x, y);
            StartAngle = startAngle;
            EndAngle = endAngle;
            Noise = noise;
        }

        public void Rotate(double r)
        {
            FOVStart.Rotate(r);
            FOVEnd.Rotate(r);
        }
    }
}
