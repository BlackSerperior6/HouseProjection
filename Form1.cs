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
            // ������������� �����������
            parameters = new ProjectionParameters();
            houseModel = new HouseModel();
            projectionCalculator = new ProjectionCalculator();
            houseRenderer = new HouseRenderer();

            // �������� ������ ����������
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
            // ����� ������
            int centerX = (this.ClientSize.Width - 250) / 2;
            int centerY = this.ClientSize.Height / 2;

            // �������� ��������������� �������
            Point3D[] transformedVertices = houseModel.GetTransformedVertices(parameters);

            // ���������� � 2D
            PointF[] projectedPoints = projectionCalculator.ProjectTo2D(transformedVertices, parameters, centerX, centerY);

            // ��������� ����� �����
            VanishingPoints vanishingPoints = projectionCalculator.CalculateVanishingPoints(parameters, centerX, centerY);

            // ������ �����
            houseRenderer.DrawHouse(g, projectedPoints, vanishingPoints, parameters, centerX, centerY);
        }

        private void OnParametersChanged(ProjectionParameters newParameters)
        {
            parameters = newParameters;
            this.Refresh();
        }
    }
}