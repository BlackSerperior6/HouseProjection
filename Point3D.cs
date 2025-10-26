using System;

namespace House3DProjection
{
    public class Point3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D Clone()
        {
            return new Point3D(X, Y, Z);
        }
    }
}