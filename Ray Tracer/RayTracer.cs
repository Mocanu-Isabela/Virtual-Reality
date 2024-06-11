using System;
using System.Runtime.InteropServices;

namespace rt
{
    class RayTracer
    {
        private Geometry[] geometries;
        private Light[] lights;

        public RayTracer(Geometry[] geometries, Light[] lights)
        {
            this.geometries = geometries;
            this.lights = lights;
        }

        private double ImageToViewPlane(int n, int imgSize, double viewPlaneSize)
        {
            var u = n * viewPlaneSize / imgSize;
            u -= viewPlaneSize / 2;
            return u;
        }

        private Intersection FindFirstIntersection(Line ray, double minDist, double maxDist)
        {
            var intersection = new Intersection();

            foreach (var geometry in geometries)
            {
                var intr = geometry.GetIntersection(ray, minDist, maxDist);

                if (!intr.Valid || !intr.Visible) continue;

                if (!intersection.Valid || !intersection.Visible)
                {
                    intersection = intr;
                }
                else if (intr.T < intersection.T)
                {
                    intersection = intr;
                }
            }

            return intersection;
        }

        private bool IsLit(Vector point, Light light)
        {
            // ADD CODE HERE: Detect whether the given point has a clear line of sight to the given light
            Line ray = new Line(light.Position, point); // the ray from the light to the point
            var intersection = FindFirstIntersection(ray, 0.0, (light.Position - point).Length()); //the first intersection between the light ray and the point
            
            return !intersection.Valid;// if the intersection exists then the point is lit
        }

        public void Render(Camera camera, int width, int height, string filename)
        {
            var image = new Image(width, height);

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++) //for each pixel of the view grid
                {
                    var x0 = camera.Position;  // position vector of camera (our view point)
                    var x1 = camera.Position + camera.Direction * camera.ViewPlaneDistance +
                             camera.Up * ImageToViewPlane(j, height, camera.ViewPlaneHeight)  +
                             (camera.Up ^ camera.Direction) * ImageToViewPlane(i, width, camera.ViewPlaneWidth);  //position vector of pixel
                    
                    /* x1 = ViewPoint + ViewDirection * PlaneDistance + ViewUp * ImageToViewPlane(iy, ih, vph) + ViewParallel * ImageToViewPlane(ix, iw, vpw) */

                    var intersection = FindFirstIntersection(new Line(x0, x1), camera.FrontPlaneDistance, camera.BackPlaneDistance); // check if there is a intersection between the line xox1 and a sphere (if there is, find the first intersection)

                    if (intersection.Geometry == null)
                    {
                        image.SetPixel(i, j, new Color()); //if there is no intersection, then we give the default colour
                    }
                    else
                    {
                        Color color = new Color(); // otherwise we make a new colour
                        Material material = intersection.Geometry.Material; // check the properties of the material

                        foreach (var light in lights) // go through all light sources
                        {
                            var lightColor = material.Ambient * light.Ambient; // start with just the ambient light 

                            if (IsLit(intersection.Position, light))
                            {
                                /*
                                 * L: light position vector (from system of reference to light source)
                                 * C: camera position vector (from system of reference to the camera)
                                 * V: intersection position vector (from system of reference to the point of intersection between the camera and the sphere)
                                 * E: vector from the intersection point to the camera (normalized)  (from the point of intersection between the camera and the sphere to the camera)
                                 * N: normal to the surface at the intersection point (normalized)  (to the tangent)
                                 * T: vector from the intersection point to the light (normalized) (from the point of intersection between the light source and the sphere to the light source)
                                 * R: reflection vector (normalized)    R = N*(N*T)*2 - T    (from the point of intersection between the light source and the sphere to the exterior)
                                 */
                                
                                var n = (intersection.Position - ((Sphere)intersection.Geometry).GetCenter()).Normalize();
                                var t = (light.Position - intersection.Position).Normalize();

                                if (n * t > 0)
                                {
                                    lightColor += material.Diffuse * light.Diffuse * (n * t); //add diffuse light on top of ambient when cos(n,t) > 0 
                                }

                                var e = (camera.Position - intersection.Position).Normalize();
                                var r = n * (n * t) * 2 - t;

                                if (e * r > 0) // if the reflection is visible by the camera
                                {
                                    lightColor += material.Specular * light.Specular * Math.Pow((e * r), material.Shininess);
                                    // the smaller the angle between e and r, the brighter the light appears to the camera
                                }

                                lightColor *= light.Intensity;
                            }

                            color += lightColor;//for each light sources, add the computed light on top of the existing one
                        }

                        image.SetPixel(i, j, color);
                    }
                }
            }

            image.Store(filename);
        }
    }
}