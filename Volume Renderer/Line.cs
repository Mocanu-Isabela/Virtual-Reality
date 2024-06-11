using System;
using System.Collections.Generic;
using System.Text;

namespace VolumeRendering
{
    public class Line
    {
        public Vector originPoint { get; set; }
        public Vector direction { get; set; }

        public Line(Vector a, Vector b)
        {
            originPoint = new Vector(a);
            direction = new Vector(b - a);
            direction.Normalize();
        }

        public Vector CoordinateToPosition(double t)
        {
            return new Vector(direction * t + originPoint);
        }
    }
}
