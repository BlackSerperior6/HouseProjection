using System.Drawing;
using System.Drawing.Drawing2D;

namespace House3DProjection
{
    public class HouseRenderer
    {
        public void DrawHouse(Graphics g, PointF[] projectedPoints, VanishingPoints vanishingPoints,
                            ProjectionParameters parameters, int centerX, int centerY)
        {
            g.Clear(Color.White);

            // Рисуем линии дома
            Pen housePen = new Pen(Color.Blue, 2);
            Pen roofPen = new Pen(Color.Red, 2);

            // Нижняя грань
            DrawLine(g, housePen, projectedPoints[0], projectedPoints[1]);
            DrawLine(g, housePen, projectedPoints[1], projectedPoints[2]);
            DrawLine(g, housePen, projectedPoints[2], projectedPoints[3]);
            DrawLine(g, housePen, projectedPoints[3], projectedPoints[0]);

            // Верхняя грань
            DrawLine(g, housePen, projectedPoints[4], projectedPoints[5]);
            DrawLine(g, housePen, projectedPoints[5], projectedPoints[6]);
            DrawLine(g, housePen, projectedPoints[6], projectedPoints[7]);
            DrawLine(g, housePen, projectedPoints[7], projectedPoints[4]);

            // Вертикальные ребра
            DrawLine(g, housePen, projectedPoints[0], projectedPoints[4]);
            DrawLine(g, housePen, projectedPoints[1], projectedPoints[5]);
            DrawLine(g, housePen, projectedPoints[2], projectedPoints[6]);
            DrawLine(g, housePen, projectedPoints[3], projectedPoints[7]);

            // Крыша
            DrawLine(g, roofPen, projectedPoints[4], projectedPoints[8]);
            DrawLine(g, roofPen, projectedPoints[8], projectedPoints[7]);
            DrawLine(g, roofPen, projectedPoints[5], projectedPoints[9]);
            DrawLine(g, roofPen, projectedPoints[9], projectedPoints[6]);
            DrawLine(g, roofPen, projectedPoints[4], projectedPoints[10]);
            DrawLine(g, roofPen, projectedPoints[10], projectedPoints[5]);
            DrawLine(g, roofPen, projectedPoints[7], projectedPoints[11]);
            DrawLine(g, roofPen, projectedPoints[11], projectedPoints[6]);

            // Рисуем вершины
            Brush vertexBrush = new SolidBrush(Color.Green);
            for (int i = 0; i < projectedPoints.Length; i++)
            {
                g.FillEllipse(vertexBrush, projectedPoints[i].X - 3, projectedPoints[i].Y - 3, 6, 6);
            }

            // Рисуем точки схода
            DrawVanishingPoints(g, vanishingPoints, centerX, centerY);

            // Подписи
            DrawLabels(g, parameters, centerX, centerY);
        }

        private void DrawLine(Graphics g, Pen pen, PointF p1, PointF p2)
        {
            g.DrawLine(pen, p1, p2);
        }

        private void DrawVanishingPoints(Graphics g, VanishingPoints vp, int centerX, int centerY)
        {
            Brush vpBrush = new SolidBrush(Color.Magenta);
            Font vpFont = new Font("Arial", 8);

            // Рисуем точки схода
            g.FillEllipse(vpBrush, vp.X.X - 5, vp.X.Y - 5, 10, 10);
            g.FillEllipse(vpBrush, vp.Z.X - 5, vp.Z.Y - 5, 10, 10);

            // Подписи точек схода
            g.DrawString("VP X", vpFont, Brushes.Magenta, vp.X.X + 10, vp.X.Y - 10);
            g.DrawString("VP Y", vpFont, Brushes.Magenta, vp.X.X + 10, vp.X.Y + 10);
            g.DrawString("VP Z", vpFont, Brushes.Magenta, vp.Z.X + 10, vp.Z.Y);

            // Линии к точкам схода (для наглядности)
            Pen vpPen = new Pen(Color.LightGray, 1) { DashStyle = DashStyle.Dash };
            g.DrawLine(vpPen, centerX, centerY, vp.X.X, vp.X.Y);
            g.DrawLine(vpPen, centerX, centerY, vp.Z.X, vp.Z.Y);
        }

        private void DrawLabels(Graphics g, ProjectionParameters parameters, int centerX, int centerY)
        {
            Font labelFont = new Font("Arial", 10);
            g.DrawString("3D Projection House with Vanishing Points", labelFont, Brushes.Black, 10, 10);
            g.DrawString($"Position: ({parameters.ObjectX}, {parameters.ObjectY}, {parameters.ObjectZ})", labelFont, Brushes.Black, 10, 30);
            g.DrawString($"Angles: F={parameters.AngleF}°, T={parameters.AngleT}°", labelFont, Brushes.Black, 10, 50);
            g.DrawString($"Viewpoint Z: {parameters.ViewpointZ}", labelFont, Brushes.Black, 10, 70);
        }
    }
}