using Simulation.Objects;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Engine
{
    public class Camera
    {
        public Step Step { get; private set; }
        public Point Location { get { return Step.Point + Step.Noise; } }
        private Ray FOVStart { get { return Step.FOVStart; } }
        private Ray FOVEnd { get { return Step.FOVEnd; } }

        public Camera(Step step)
        {
            Step = step;
        }

        public void Draw(Graphics g, Pen cameraPen = null, Pen rayPen = null)
        {
            Step.Point.Draw(g, Pens.Black);
            Location.Draw(g, cameraPen);
            FOVStart.Draw(g, rayPen);
            FOVEnd.Draw(g, rayPen);
        }

        public void LookAt(double angle)
        {
            //TODO: change angle directions 
        }

        public List<Ray> GetFOVRays()
        {
            IEnumerable<int> angles;
            if (FOVStart.Angle <= FOVEnd.Angle)
            {
                angles = Enumerable.Range((int)FOVStart.Angle+1, (int) (FOVEnd.Angle - FOVStart.Angle + 1));
            }
            else
            {
                var a1 = Enumerable.Range(0, (int)FOVEnd.Angle+1);
                var a2 = Enumerable.Range((int)FOVStart.Angle, 359-(int)FOVStart.Angle+1);
                angles = a2.Concat(a1);
            }
            return angles.Select(a => new Ray(Location, a)).ToList();
        }
    }
}
