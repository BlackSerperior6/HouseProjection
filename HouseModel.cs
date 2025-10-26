namespace House3DProjection
{
    public class HouseModel
    {
        public Point3D[] Vertices { get; private set; }

        public HouseModel()
        {
            InitializeVertices();
        }

        private void InitializeVertices()
        {
            // Определяем вершины дома (куб 200x200x200 + крыша)
            Vertices = new Point3D[]
            {
                // Основание (нижняя грань)
                new Point3D(-100, -100, -100), // 0: задний-левый-нижний
                new Point3D(100, -100, -100),  // 1: задний-правый-нижний
                new Point3D(100, 100, -100),   // 2: передний-правый-нижний
                new Point3D(-100, 100, -100),  // 3: передний-левый-нижний
                
                // Верхняя грань
                new Point3D(-100, -100, 100),  // 4: задний-левый-верхний
                new Point3D(100, -100, 100),   // 5: задний-правый-верхний
                new Point3D(100, 100, 100),    // 6: передний-правый-верхний
                new Point3D(-100, 100, 100),   // 7: передний-левый-верхний
                
                // Крыша
                new Point3D(-100, 0, 200),     // 8: левая вершина крыши
                new Point3D(100, 0, 200),      // 9: правая вершина крыши
                new Point3D(0, -100, 200),     // 10: задняя вершина крыши
                new Point3D(0, 100, 200)       // 11: передняя вершина крыши
            };
        }

        public Point3D[] GetTransformedVertices(ProjectionParameters parameters)
        {
            Point3D[] transformed = new Point3D[Vertices.Length];

            // Матрица поворота
            double[,] rotationMatrix = {
                { Math.Cos(parameters.AngleTRad), Math.Sin(parameters.AngleTRad) * Math.Sin(parameters.AngleFRad), Math.Sin(parameters.AngleTRad) * Math.Cos(parameters.AngleFRad) },
                { 0, Math.Cos(parameters.AngleFRad), -Math.Sin(parameters.AngleFRad) },
                { -Math.Sin(parameters.AngleTRad), Math.Cos(parameters.AngleTRad) * Math.Sin(parameters.AngleFRad), Math.Cos(parameters.AngleTRad) * Math.Cos(parameters.AngleFRad) }
            };

            for (int i = 0; i < Vertices.Length; i++)
            {
                // Применяем позицию объекта
                double x = Vertices[i].X + parameters.ObjectX;
                double y = Vertices[i].Y + parameters.ObjectY;
                double z = Vertices[i].Z + parameters.ObjectZ;

                // Поворачиваем вершину
                double xRot = rotationMatrix[0, 0] * x + rotationMatrix[0, 1] * y + rotationMatrix[0, 2] * z;
                double yRot = rotationMatrix[1, 0] * x + rotationMatrix[1, 1] * y + rotationMatrix[1, 2] * z;
                double zRot = rotationMatrix[2, 0] * x + rotationMatrix[2, 1] * y + rotationMatrix[2, 2] * z;

                transformed[i] = new Point3D(xRot, yRot, zRot);
            }

            return transformed;
        }
    }
}