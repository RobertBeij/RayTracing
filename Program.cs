using System;
using System.IO;
using System.Threading.Tasks;
using System.Numerics;
using Point3 = Vec3;
using Color = Vec3;

//TODO: Check Operator Overloading for Vec3 class

//Left at 6.2


namespace raytracing
{
    class Program
    {
        static async Task Main(string[] args)
        {

            // Image
            var aspectRatio = 16.0 / 9.0;
            var imageWidth = 400;
            var imageHeight = (int)(imageWidth / aspectRatio);

            //Camera
            var viewportHeight = 2.0;
            var viewportWidth = aspectRatio * viewportHeight;
            var focalLength = 1.0;

            var origin = new Point3(0, 0, 0);
            var horizontal = new Vec3(viewportWidth, 0, 0);
            var vertical = new Vec3(0, viewportHeight, 0);
            var lowerLeftCorner = origin.Subtract(horizontal.Divide(2)).Subtract(vertical.Divide(2)).Subtract(new Vec3(0, 0, focalLength));

            //Render
            using(StreamWriter file = new StreamWriter("image.ppm"))
            {

            await file.WriteLineAsync($"P3 \n{imageWidth} {imageHeight} \n255");

                for (int j = imageHeight - 1; j >= 0; --j)
                {
                    System.Console.WriteLine($"Scanlines remaining: {j} ");
                    for (int i = 0; i < imageWidth; ++i)
                    {
                        var u = (double)i / (imageWidth - 1);
                        var v = (double)j / (imageHeight - 1);
                        Ray r = new Ray(
                            origin,
                            lowerLeftCorner.Add(horizontal.Multiply(u))
                            .Add(vertical.Multiply(v))
                            .Subtract(origin));
                        Color pixel = RayColor(r);

                        await file.WriteLineAsync(Vec3.WriteColor(pixel));
                    }
                }
            }
        }

        public static Color RayColor(Ray r)
        {
            var t = HitsShere(new Vec3(0, 0, -1), 0.5, r);
            if (t > 0.0) {
                Vec3 N = r.At(t).UnitVector().Subtract(new Vec3(0, 0, -1));
                return new Color(N.x() + 1, N.y() + 1, N.z() + 1).Multiply(0.5);
            }

            Vec3 unitDirection = r.direction.UnitVector();
            t = 0.5 * (unitDirection.y() + 1.0);
            return new Color(1.0, 1.0, 1.0).Multiply(1.0 - t).Add(new Color(0.5, 0.7, 1.0).Multiply(t));
        }

        public static double HitsShere(Vec3 center, double radius, Ray r) {
            Vec3 oc = r.origin.Subtract(center);
            var a = r.direction.Dot(r.direction);
            var b = 2.0 * oc.Dot(r.direction);
            var c = oc.Dot(oc) - radius * radius;
            var discriminant = b * b - 4 * a * c;
            if (discriminant < 0) {
                return -1.0;
            } else {
                return (-b - Math.Sqrt(discriminant)) / (2.0 * a);
            }
        }
    }
}
