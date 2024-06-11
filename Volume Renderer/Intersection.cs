using System;
using System.Collections.Generic;
using System.Text;

namespace VolumeRendering
{
    public class Intersection
    {
        public bool Valid { get; set; }
        public bool Visible { get; set; }
        public double intersectionPoint { get; set; }
        public Vector positionVector { get; set; }
        public Geometry Geometry { get; set; }
        public Line Line { get; set; }

        public Intersection()
        {
            Geometry = null;
            Line = null;
            Valid = false;
            Visible = false;
            intersectionPoint = 0;
            positionVector = null;
        }

        public Intersection(bool valid, bool visible, Geometry geometry, Line line, double t)
        {
            Geometry = geometry;
            Line = line;
            Valid = valid;
            Visible = visible;
            intersectionPoint = t;
            positionVector = Line.CoordinateToPosition(t);
        }
    }
}
