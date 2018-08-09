using Engine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simulation.Helpers
{
    public static class Eq
    {
        public static double ToRad(this double degree)
        {
            return degree * Math.PI / 180;
        }

        public static double ToDeg(this double radians)
        {
            return radians * 180 / Math.PI;
        }

        public static double Length(this Point p)
        {
            return Math.Sqrt(Math.Pow(p.X, 2) + Math.Pow(p.Y, 2));
        }

        public static Point ToUnitVector(this Point p)
        {
            return p / p.Length();
        }


        public static double GetAngleBetween(this Point p1, Point p2)
        {
            return Math.Atan2(p2.Y - p1.Y, p2.X - p1.X).ToDeg();
        }

        public static Tuple<SceneObject, Point> GetNearestIntersect(this Ray ray, List<SceneObject> objects)
        {
            IEnumerable<Tuple<SceneObject, Point>> pts = objects.Select(obj =>
            {
                if (obj is Circle)
                {
                    var pt = ray.RayCircleIntersections((Circle)obj);
                    if (pt.Item1 == 1)
                    {
                        return new Tuple<SceneObject, Point> (obj, pt.Item2);
                    }
                    else if (pt.Item1 == 2)
                    {
                        return new Tuple<SceneObject, Point>(obj, 
                            ray.Start.DistanceTo(pt.Item2) < ray.Start.DistanceTo(pt.Item3)
                            ? pt.Item2 : pt.Item3
                            );
                    }
                    return new Tuple<SceneObject, Point>(null, null);
                }
                else if (obj is Polygon)
                {
                    return ray.GetNearestIntersect(new List<SceneObject>(((Polygon)obj).Lines));
                }
                else if (obj is LineSegment)
                {
                    return new Tuple<SceneObject, Point>(obj, ray.RayLineSegmentIntersection((LineSegment)obj));
                }
                
                return null;
            }).Where(p => p.Item2 != null);

            if (pts.Count() >= 1)
            {
                var pt = pts
                    .Select(p => new Tuple<SceneObject, Point, double>(p.Item1, p.Item2, p.Item2.DistanceTo(ray.Start)))
                    .OrderBy(p => p.Item3).First();
                return new Tuple<SceneObject, Point>(pt.Item1, pt.Item2);
            }
            return new Tuple<SceneObject, Point>(null, null);
        }


        public static Tuple<int, Point, Point> RayCircleIntersections(this Ray ray, Circle circle)
        {
            Point direction = ray.Direction;
            Point normalized = direction.Normalized();

            Point h = circle.Center-ray.Start;

            double lf = normalized * h;
            double s = Math.Pow(circle.Radius, 2) - (h * h) + Math.Pow(lf, 2);

            
            if (s < 0) return new Tuple<int, Point, Point> (0, null, null);
            s = Math.Sqrt(s);
            int r = 0;
            if (lf < s)
            {
                if (lf+s >= 0)
                {
                    s = -s;
                    r = 1;
                }
            } else
            {
                r = 2;
            }
            var r1 = ray.Start + (normalized * (lf - s));
            var r2 = ray.Start + (normalized * (lf + s));
            return new Tuple<int, Point, Point>(r, r1, r2);

        }

        public static Point RayLineSegmentIntersection(this Ray ray, LineSegment lineSegment)
        {
            return RayLineSegmentIntersection(ray.X, ray.Y, ray.Direction.X, ray.Direction.Y, lineSegment.Start.X, lineSegment.Start.Y, lineSegment.End.X, lineSegment.End.Y);
        }

        // https://gamedev.stackexchange.com/a/109425
        public static Point RayLineSegmentIntersection(double x, double y, double dx, double dy, double x1, double y1, double x2, double y2)
        {
            double r, s, d;
            //Make sure the lines aren't parallel, can use an epsilon here instead
            // Division by zero in C# at run-time is infinity. In JS it's NaN
            if (dy / dx != (y2 - y1) / (x2 - x1))
            {
                d = ((dx * (y2 - y1)) - dy * (x2 - x1));
                if (d != 0)
                {
                    r = (((y - y1) * (x2 - x1)) - (x - x1) * (y2 - y1)) / d;
                    s = (((y - y1) * dx) - (x - x1) * dy) / d;
                    if (r >= 0 && s >= 0 && s <= 1)
                    {
                        return new Engine.Point(x + r * dx, y + r * dy);
                    }
                }
            }
            return null;
        }
    }
}
