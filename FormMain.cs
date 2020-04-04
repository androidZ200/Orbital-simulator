using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace орбитальная_механика
{
    public partial class FormMain : Form
    {
        public SimulationSpace space;
        bool MousDownCheck = false;
        bool MousButtonLeft;
        Point MousDownPoint;
        Point MousPrevPoint;
        SpaceBody currentBody;
        public IControl drawingShip;

        Size GetSizePictureBox()
        {
            return pictureBox1.Size;
        }

        public FormMain()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            space = new SimulationSpace(this, GetSizePictureBox);
        }
        private void StartStopButton_Click(object sender, EventArgs e)
        {
            if (!space.Play)
            {
                StartStopButton.Text = "Stop";
                space.Start();
            }
            else
            {
                StartStopButton.Text = "Start";
                space.Stop();
                Thread.Sleep(60);
            }
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            if (space.Play)
                StartStopButton_Click(sender, e);
            FormAddBody form = new FormAddBody(this);
            form.ShowDialog();
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            space.Clear();
            Thread.Sleep(60);
            StartStopButton.Text = "Start";
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            space.Delete();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            MousDownCheck = true;
            MousButtonLeft = e.Button == MouseButtons.Left;
            MousDownPoint = MousPrevPoint = e.Location;
            currentBody = space.FindBody(MousDownPoint);
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (MousDownCheck && currentBody != null)
            {
                if (MousButtonLeft)
                    space.MoveBody(currentBody, new Point(e.X - MousPrevPoint.X, e.Y - MousPrevPoint.Y));
                else
                    space.ChangeSpeedVector(currentBody, e.Location);
            }
            else if (MousDownCheck)
                space.MoveSpace(new Point(MousPrevPoint.X - e.X, MousPrevPoint.Y - e.Y));
            MousPrevPoint = e.Location;
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            MousDownCheck = false;
            if (e.Location == MousDownPoint && !space.Play && currentBody != null)
            {
                FormChangeBody form = new FormChangeBody(this, currentBody);
                form.ShowDialog();
            }
            currentBody = null;
        }
        private void LenghtTrackBar_Scroll(object sender, EventArgs e)
        {
            LenghtLabel.Text = Convert.ToString(LenghtTrailTrackBar.Value);
            space.GetSpace().LengthTrail = LenghtTrailTrackBar.Value;
        }
        private void AdditionallyButton_Click(object sender, EventArgs e)
        {
            if (space.Play)
                StartStopButton_Click(sender, e);
            FormEveryBody form = new FormEveryBody(this);
            form.ShowDialog();
        }
        private void SpeedVectorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            space.SpeedVector = SpeedVectorCheckBox.Checked;
        }
        private void SpeedTrackBar_Scroll(object sender, EventArgs e)
        {
            space.SpeedSimulation(Math.Exp(SpeedTrackBar.Value / 2));
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (drawingShip != null)
                drawingShip.KeyDown(e.KeyCode);
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (drawingShip != null)
                drawingShip.KeyUp(e.KeyCode);
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex;
            switch(i)
            {
                case 0:
                    space.NewBackground(new SpaceBackgroundBlack());
                    break;
                case 1:
                    space.NewBackground(new SpaceBackgroundStars(10000, 10000));
                    break;
                case 2:
                    space.NewBackground(new SpaceBackgroundRetro());
                    break;
                case 3:
                    space.NewBackground(new SpaceBackgroundStaticStars(10000, 10000));
                    break;
                case 4:
                    space.NewBackground(new SpaceBackgroundStaticRetro());
                    break;
                case 5:
                    space.NewBackground(new SpaceBackgroundCentral(space.GetSpace().bodies));
                    break;
                case 6:
                    space.NewBackground(new SpaceBackgroundRadial(space.GetSpace().bodies));
                    break;
                case 7:
                    space.NewBackground(new SpaceBackgroundColor(space.GetSpace().bodies));
                    break;
            }
        }
        private void RadarCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            space.Radar = RadarCheckBox.Checked;
        }
        private void TimeTrackBar_Scroll(object sender, EventArgs e)
        {
            TimeLabel.Text = Convert.ToString(LengthFutureTrackBar.Value);
            space.GetSpace().LengthFuture = LengthFutureTrackBar.Value;
        }
    }
}