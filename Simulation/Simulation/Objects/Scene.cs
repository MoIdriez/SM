using Simulation.Helpers;
using Simulation.Objects;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Engine
{
    public class Scene
    {
        public Camera Camera { get; private set; }
        public Graphics Graphics { get; private set; }        
        public List<SceneObject> SceneObjects { get; private set; }

        public Scene(Camera camera, Graphics graphics)
        {
            Camera = camera;
            Graphics = graphics;            
            SceneObjects = new List<SceneObject>();
        }

        public Scene(Camera camera, Graphics graphics, List<SceneObject> sceneObjects)
        {
            Camera = camera;
            Graphics = graphics;
            SceneObjects = sceneObjects ?? new List<SceneObject>();
        }

        public void Add(SceneObject sceneObject)
        {
            SceneObjects.Add(sceneObject);
        }
        public void Add(List<SceneObject> sceneObjects)
        {
            SceneObjects = sceneObjects;
        }
        
        public void Draw()
        {
            Camera.Draw(Graphics);
            SceneObjects.ForEach(o => o.Draw(Graphics));

            var state = GenerateState();
            state.ForEach(s => s.Draw(Graphics));

            DrawStateValues(state);
        }

        public List<Fov> GenerateState()
        {
            var fov = Camera.GetFOVRays();
            return fov.Select(vp =>
            {
                var intersect = vp.GetNearestIntersect(SceneObjects);
                return new Fov(vp, intersect);
            }).ToList();
        }

        public List<Point> GetExpectedIntersectingPoints()
        {
            var points = Camera.GetFOVRays().Select(vp => vp.GetNearestIntersect(SceneObjects).Item2);
            return points.Where(p => p != null).Select(p => p - Camera.Step.Noise).ToList();
        }

        private void DrawStateValues(List<Fov> fovs)
        {
            int counter = 0;
            int side = 10;

            fovs.ForEach(f => {                
                Graphics.DrawRectangle(new Pen(f.Color), 0 + side * counter, 0, side, side);
                counter++;
            });
        }
    }
}
