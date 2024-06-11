using System;

namespace rt
{
    public class Sphere : Geometry
    {
        private Vector Center { get; set; }
        private double Radius { get; set; }

        public Sphere(Vector center, double radius, Material material, Color color) : base(material, color)
        {
            Center = center;
            Radius = radius;
        }
        
        public Vector GetCenter()
        {
            return Center;
        }

        public override Intersection GetIntersection(Line line, double minDist, double maxDist)
        {
            // ADD CODE HERE: Calculate the intersection between the given line and this sphere
            //if they intersect, create an intersection object and return it
            
            var pointClosestToCenter = (Center - line.X0) * line.Dx;// the distance between the camera view and the point on the line closest to the center of the sphere
            // (the point on the visual ray (line from camera) that is the closest to the center of the sphere)

            var pointReachedAfterTime = line.CoordinateToPosition(pointClosestToCenter);// the point from above (the point on the line closest to the center of the sphere)

            var lengthBetweenCenterAndPoint = (Center - pointReachedAfterTime).Length();// the distance from the center of the sphere to the point from above

            var distanceBetweenPoints = Math.Sqrt(Radius * Radius - lengthBetweenCenterAndPoint * lengthBetweenCenterAndPoint);
            //pitagora => the result is the distance between the point closest to the center and the first intersection point(pointReachedAfterTime si punctul de pe discul sferei care se afla si pe visual ray)

            if (pointClosestToCenter > minDist && pointClosestToCenter < maxDist && lengthBetweenCenterAndPoint <= Radius) // the third requirement is true when the point is in the sphere or on the disc of the sphere
            {
                return new Intersection(true, true, this, line, pointClosestToCenter-distanceBetweenPoints); //pointClosestToCenter-distanceBetweenPoints = the first intersection
            }
            return new Intersection();
        }

        public override Vector Normal(Vector v)
        {
            var n = v - Center;
            n.Normalize();
            return n;
        }
    }
}