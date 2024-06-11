using System;
using System.IO;
using System.Threading.Tasks;

namespace VolumeRendering
{
    class Program
    {
        static void Main(string[] args)
        {
            // Cleanup
            const string frames = "frames";
            if (Directory.Exists(frames))
            {
                var d = new DirectoryInfo(frames);
                foreach (var file in d.EnumerateFiles("*.png"))
                {
                    file.Delete(); // cleaning up the directory
                }
            }
            Directory.CreateDirectory(frames);

            var lights = new Light[]
            {
                new Light(
                    new Vector(-60.0, -5.0, -6.0), // position
                    new Color(0.3, 0.2, 0.2, 1.0), // ambient
                    new Color(0.3, 0.2, 0.2, 1.0), // diffuse
                    new Color(0.3, 0.2, 0.2, 1.0), // specular
                    0.6 // intensity
                )
            };
            Asset asset = new Asset(); // matrix of numbers
            var rt = new RayTracer(lights, asset);

            const int width = 800;
            const int height = 600;

            // Go around the middle of the scene and generate a frame for each view
            var middle = new Vector(asset.getCenterX(), asset.getCenterY(), asset.getCenterZ());

            var up = new Vector(-Math.Sqrt(0.125), -Math.Sqrt(0.75), Math.Sqrt(0.125)).Normalize(); // where the up direction is
            var first = (middle ^ up).Normalize(); // direction from where to look
            const double dist = 170.0; //distance from middle to camera
            const int n = 25; // how many frames
            const double step = 360.0 / n; // how much to move to the next frame around the middle

            var tasks = new Task[n]; // tasks to compute the frames in parallel
            for (var i = 0; i < n; i++) // for each frame
            {
                var ind = new[] { i }; // remember the index
                tasks[i] = Task.Run(() =>
                {
                    var k = ind[0]; // i
                    var a = (step * k) * Math.PI / 180.0; // the angle from which we are looking at
                    var ca = Math.Cos(a);
                    var sa = Math.Sin(a);

                    var dir = first * ca + (up ^ first) * sa + up * (up * first) * (1.0 - ca); // the dir with that angle

                    var camera = new Camera(
                        middle + dir * dist, // camera position obtained from adding middle vector and dir vector
                        dir * -1.0,
                        up,
                        65.0,
                        160.0,
                        120.0,
                        0.0,
                        1000.0
                    ) ;

                    var filename = frames + "/" + $"{k + 1:000}" + ".png";

                    Console.WriteLine($"Computing frame {k + 1}/{n}");
                    rt.Render(camera, width, height, filename);
                    Console.WriteLine($"Frame {k + 1}/{n} completed");
                });
            }

            Task.WaitAll(tasks);
        }
    }
}
