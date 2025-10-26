using System.Drawing;

namespace House3DProjection
{
    public class ProjectionCalculator
    {
        public PointF[] ProjectTo2D(Point3D[] vertices3D, ProjectionParameters parameters, int centerX, int centerY)
        {
            PointF[] projectedPoints = new PointF[vertices3D.Length];
            double scale = 2.0;

            for (int i = 0; i < vertices3D.Length; i++)
            {
                // Перспективная проекция
                double perspective = parameters.ViewpointZ / (parameters.ViewpointZ + vertices3D[i].Z);
                double xProj = vertices3D[i].X * perspective * scale;
                double yProj = vertices3D[i].Y * perspective * scale;

                projectedPoints[i] = new PointF(
                    centerX + (float)xProj,
                    centerY - (float)yProj // Инвертируем Y для правильной ориентации
                );
            }

            return projectedPoints;
        }

        public VanishingPoints CalculateVanishingPoints(ProjectionParameters parameters, int centerX, int centerY)
        {
            double scale = 2.0;

            // Точки схода для осей X, Y, Z
            double vpX = centerX + (Math.Cos(parameters.AngleTRad) * 1000 * scale);
            double vpY = centerY - (Math.Sin(parameters.AngleFRad) * 1000 * scale);
            double vpZ = centerX - (Math.Sin(parameters.AngleTRad) * 1000 * scale);

            return new VanishingPoints
            {
                X = new PointF((float)vpX, (float)vpY),
                Y = new PointF((float)vpX, (float)vpY),
                Z = new PointF((float)vpZ, centerY)
            };
        }
    }

    public class VanishingPoints
    {
        public PointF X { get; set; }
        public PointF Y { get; set; }
        public PointF Z { get; set; }
    }
}