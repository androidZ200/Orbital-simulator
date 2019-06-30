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
        public SpaceShip drawingShip;

        Size GetSizePictureBox()
        {
            return pictureBox1.Size;
        }
        void DrawSpace(Bitmap picture)
        {
                Invoke((Action)(() => 
                {
                    pictureBox1.Image = picture;
                }));
        }

        public FormMain()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            space = new SimulationSpace(DrawSpace, GetSizePictureBox);
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
            SpaceBodyCountLabel.Text = "0";
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
            LabelPointX.Text = Convert.ToString(e.X + space.GetSpace().Beginning.X);
            LabelPointY.Text = Convert.ToString(e.Y + space.GetSpace().Beginning.Y);
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
            SpaceBodyCountLabel.Text = Convert.ToString(space.AllBodies().Length);
        }
        private void LenghtTrackBar_Scroll(object sender, EventArgs e)
        {
            LenghtLabel.Text = Convert.ToString(LenghtTrackBar.Value);
            space.GetSpace().LengthTrail = LenghtTrackBar.Value;
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
            space.SpeedSimulation(SpeedTrackBar.Value);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (drawingShip != null)
            {
                if (e.KeyCode == Keys.W) drawingShip.Gas = true;
                if (e.KeyCode == Keys.A) drawingShip.RotateLeft = true;
                if (e.KeyCode == Keys.D) drawingShip.RotateRight = true;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (drawingShip != null)
            {
                if (e.KeyCode == Keys.W) drawingShip.Gas = false;
                if (e.KeyCode == Keys.A) drawingShip.RotateLeft = false;
                if (e.KeyCode == Keys.D) drawingShip.RotateRight = false;
            }
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
            }
        }
    }
}