namespace House3DProjection
{
    public class ProjectionParameters
    {
        public double ObjectX { get; set; } = 0;
        public double ObjectY { get; set; } = 0;
        public double ObjectZ { get; set; } = 0;
        public double AngleF { get; set; } = 30; // угол вокруг OX в градусах
        public double AngleT { get; set; } = 45; // угол вокруг OY в градусах
        public double ViewpointZ { get; set; } = 500; // Z координата точки наблюдения

        public double AngleFRad => AngleF * Math.PI / 180.0;
        public double AngleTRad => AngleT * Math.PI / 180.0;
    }
}