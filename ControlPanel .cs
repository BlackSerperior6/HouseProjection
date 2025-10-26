using System;
using System.Drawing;
using System.Windows.Forms;

namespace House3DProjection
{
    public class ControlPanel : Panel
    {
        public event Action<ProjectionParameters> ParametersChanged;

        private ProjectionParameters parameters;

        public ControlPanel(ProjectionParameters initialParameters)
        {
            parameters = initialParameters;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Right;
            this.Width = 250;
            this.BackColor = Color.LightGray;

            int yPos = 20;

            // Позиция X
            CreateLabelAndTrackBar("Позиция X:", -300, 300, (int)parameters.ObjectX, yPos,
                (val) => { parameters.ObjectX = val; OnParametersChanged(); });
            yPos += 60;

            // Позиция Y
            CreateLabelAndTrackBar("Позиция Y:", -300, 300, (int)parameters.ObjectY, yPos,
                (val) => { parameters.ObjectY = val; OnParametersChanged(); });
            yPos += 60;

            // Позиция Z
            CreateLabelAndTrackBar("Позиция Z:", -300, 300, (int)parameters.ObjectZ, yPos,
                (val) => { parameters.ObjectZ = val; OnParametersChanged(); });
            yPos += 60;

            // Угол F (OX)
            CreateLabelAndTrackBar("Угол F (OX):", 0, 360, (int)parameters.AngleF, yPos,
                (val) => { parameters.AngleF = val; OnParametersChanged(); });
            yPos += 60;

            // Угол T (OY)
            CreateLabelAndTrackBar("Угол T (OY):", 0, 360, (int)parameters.AngleT, yPos,
                (val) => { parameters.AngleT = val; OnParametersChanged(); });
            yPos += 60;

            // Z точки наблюдения
            CreateLabelAndTrackBar("Zс:", 100, 1000, (int)parameters.ViewpointZ, yPos,
                (val) => { parameters.ViewpointZ = val; OnParametersChanged(); });
        }

        private void CreateLabelAndTrackBar(string labelText, int min, int max, int value, int yPos, Action<double> valueChanged)
        {
            Label label = new Label();
            label.Text = labelText;
            label.Location = new Point(10, yPos);
            label.Width = 200;
            this.Controls.Add(label);

            Label valueLabel = new Label();
            valueLabel.Text = value.ToString();
            valueLabel.Location = new Point(180, yPos);
            valueLabel.Width = 50;
            this.Controls.Add(valueLabel);

            TrackBar trackBar = new TrackBar();
            trackBar.Minimum = min;
            trackBar.Maximum = max;
            trackBar.Value = value;
            trackBar.Location = new Point(10, yPos + 10);
            trackBar.Width = 200;
            trackBar.ValueChanged += (s, e) =>
            {
                valueLabel.Text = trackBar.Value.ToString();
                valueChanged(trackBar.Value);
            };
            this.Controls.Add(trackBar);
        }

        private void OnParametersChanged()
        {
            ParametersChanged?.Invoke(parameters);
        }
    }
}