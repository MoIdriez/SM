﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace Engine
{
    public class Polygon: SceneObject
    {
        public Pen Pen { get; set; }
        public List<LineSegment> Lines { get; private set; }

        public Polygon(List<LineSegment> lines, Pen pen = null)
        {
            Lines = lines;
            Pen = pen ?? GetDefaultPen();
        }

        public void Draw(Graphics g, Pen pen = null)
        {
            pen = pen ?? Pen;
            Lines.ForEach(l => l.Draw(g, pen));
        }
        public Pen GetDefaultPen()
        {
            return null;
        }
    }
}
