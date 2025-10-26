using System.Drawing;
using System.Windows.Forms;

namespace House3DProjection
{
    public partial class Form1 : Form
    {
        private ProjectionParameters parameters;
        private HouseModel houseModel;
        private ProjectionCalculator projectionCalculator;
        private HouseRenderer houseRenderer;
        private ControlPanel controlPanel;

        public Form1()
        {
            InitializeComponent();
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            // Инициализация компонентов
            parameters = new ProjectionParameters();
            houseModel = new HouseModel();
            projectionCalculator = new ProjectionCalculator();
            houseRenderer = new HouseRenderer();

            // Создание панели управления
            controlPanel = new ControlPanel(parameters);
            controlPanel.ParametersChanged += OnParametersChanged;
            this.Controls.Add(controlPanel);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawScene(e.Graphics);
        }

        private void DrawScene(Graphics g)
        {
            // Центр экрана
            int centerX = (this.ClientSize.Width - 250) / 2;
            int centerY = this.ClientSize.Height / 2;

            // Получаем преобразованные вершины
            Point3D[] transformedVertices = houseModel.GetTransformedVertices(parameters);

            // Проецируем в 2D
            PointF[] projectedPoints = projectionCalculator.ProjectTo2D(transformedVertices, parameters, centerX, centerY);

            // Вычисляем точки схода
            VanishingPoints vanishingPoints = projectionCalculator.CalculateVanishingPoints(parameters, centerX, centerY);

            // Рисуем сцену
            houseRenderer.DrawHouse(g, projectedPoints, vanishingPoints, parameters, centerX, centerY);
        }

        private void OnParametersChanged(ProjectionParameters newParameters)
        {
            parameters = newParameters;
            this.Refresh();
        }
    }
}