using VolumeRendering;
using System;

namespace VolumeRendering
{
    class RayTracer
    {
        private Light[] lights;
        private Asset asset;
        private double maximumSamplingSteps = 1000;
        private double samplingStep = 1;

        public RayTracer(Light[] lights, Asset asset)
        {
            this.lights = lights;
            this.asset = asset;
        }

        private double ImageToViewPlane(int n, int imgSize, double viewPlaneSize)
        {
            var u = n * viewPlaneSize / imgSize; // get the value on viewplane
            u -= viewPlaneSize / 2; // move it from center to normal point
            return u;
        }

        public void Render(Camera camera, int width, int height, string filename)
        {
            var image = new Image(width, height); // our frame

            var viewParallel = (camera.Up ^ camera.Direction).Normalize(); // unit vector parallel with view plane
            var vectorCameraToPlane = camera.Direction * camera.ViewPlaneDistance; // vector from camera to plane

            for(var i = 0; i < width; i++)
            {
                for(var j = 0; j < height; j++) // going through every pixel
                {
                    Vector currentPixel = camera.Position + vectorCameraToPlane +
                        camera.Up * ImageToViewPlane(j, height, camera.ViewPlaneHeight) +
                        viewParallel * ImageToViewPlane(i, width, camera.ViewPlaneWidth); // calculate current pixel in the view plane

                    Line rayThroughCurrentPixel = new Line(camera.Position, currentPixel); // the ra

                    bool hasAlreadyReachedTheAsset = false;
                    double currentStep = 1;
                    Color currentColor = new Color();
                    double currentAlpha = 0; // calculate transparency
                    while(currentStep < maximumSamplingSteps) // we are not going past 1000 because there is no point
                    {
                        Vector samplePosition = rayThroughCurrentPixel.CoordinateToPosition(currentStep); // the position where we currently check for a voxel of the figure
                        if(!asset.isPointInsideAsset(samplePosition))
                        {

                            if (!hasAlreadyReachedTheAsset)
                            {
                                // if we haven't reached the asset yet, do one more step
                                currentStep += samplingStep;
                                continue;
                            }
                            else
                            {
                                // if we are not inside the asset and the asset was already reached once, it will never be reached again and the algorithm stops
                                break;
                            }
                        }

                        // we are inside the asset
                        hasAlreadyReachedTheAsset = true;
                        double valueInCurrentCube = asset.getValueFromPosition(samplePosition) / 255.0; // shows the intensity of the colour of this voxel

                        if(valueInCurrentCube >= 0.0/255.0 && valueInCurrentCube < 19.0/255.0) // if its between 0 and 19 we assume its white
                        {
                            // blank
                            image.SetPixel(i, j, Color.white());
                        } else
                        {
                            currentAlpha += valueInCurrentCube;
                            if (currentAlpha >= 1.0)
                            {
                                currentAlpha = 1.0;
                            }

                            Color newColor = new Color(valueInCurrentCube, valueInCurrentCube, valueInCurrentCube, valueInCurrentCube);
                            Material material = new Material(
                                new Color(70.0 / 256.0, 150.0 / 256.0, 100.0 / 100.0, 1.0),
                                new Color(50.0 / 256.0, 150.0 / 256.0, 100.0 / 100.0, 1.0),
                                new Color(110.0 / 256.0, 150.0 / 256.0, 100.0 / 100.0, 1.0),
                                6
                            );
                            newColor *= 1.0 - currentAlpha;

                            foreach (Light light in lights)
                            {
                                newColor += material.Ambient * light.Ambient;

                                double a = asset.getValueFromPosition(samplePosition + new Vector(1, 0, 0));
                                double b = asset.getValueFromPosition(samplePosition + new Vector(-1, 0, 0));
                                double c = asset.getValueFromPosition(samplePosition + new Vector(0, 1, 0));
                                double d = asset.getValueFromPosition(samplePosition + new Vector(0, -1, 0));
                                double e = asset.getValueFromPosition(samplePosition + new Vector(0, 0, 1));
                                double f = asset.getValueFromPosition(samplePosition + new Vector(0, 0, -1));

                                // we make a normal with whatever values we have around
                                Vector N = samplePosition.Normalize();
                                if (N * rayThroughCurrentPixel.direction > 0)
                                {
                                    N *= -1.0;
                                }
                                Vector E = (camera.Position - samplePosition).Normalize();
                                Vector T = (light.Position - samplePosition).Normalize();
                                Vector R = (N * (N * T) * 2 - T).Normalize();
                                if (N * T > 0)
                                {
                                    newColor += material.Diffuse * light.Diffuse * (N * T);
                                }
                                if (E * R > 0)
                                {
                                    newColor += material.Specular * light.Specular *
                                        Math.Pow(E * R, material.Shininess);
                                }
                                newColor *= light.Intensity;
                            }

                            currentColor += newColor; // we stack on the current colour the new colour we got

                            image.SetPixel(i, j, currentColor);
                            if (currentAlpha >= 1.0)
                            {
                                break;
                            }
                        }

                        currentStep += samplingStep;
                    }

/*                    Console.WriteLine(hasAlreadyReachedTheAsset ? "Finished ray sampling with touching the asset" : "Finished without touching the asset");
*/                }
            }
            image.Store(filename);
        }
    }
}
