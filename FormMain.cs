using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using орбитальная_механика.Properties;

namespace орбитальная_механика
{
    public partial class FormMain : Form
    {
        public List<SpaceBody> spaceBody = new List<SpaceBody>();
        object Lock = new object();
        bool Stop = true;
        bool MousDownCheck = false;
        bool MousButtonLeft;
        Thread t1;
        Point MousDownPoint;
        Point previousPoint;
        public Point Beginning = new Point(0, 0);
        SpaceBody CurrentBody;
        int LenghtTrail = 50;
        int SpeedTime = 25;


        private void Simulation()
        {
            int Counter = 0;
            while (!Stop)
            {
                Counter++;
                for (int i = 0; i < spaceBody.Count; i++)
                    for (int j = 0; j < spaceBody.Count; j++)
                        if (!(spaceBody[i].pointX == spaceBody[j].pointX && spaceBody[i].pointY == spaceBody[j].pointY))
                        {
                            double R3 = Math.Pow((spaceBody[j].pointX - spaceBody[i].pointX) * (spaceBody[j].pointX - spaceBody[i].pointX) +
                                (spaceBody[j].pointY - spaceBody[i].pointY) * (spaceBody[j].pointY - spaceBody[i].pointY), 3.0 / 2);
                            if (R3 >= 64)
                            {
                                if (!spaceBody[i].Static)
                                {
                                    spaceBody[i].speedX += spaceBody[j].Weight * (spaceBody[j].pointX - spaceBody[i].pointX) / R3;
                                    spaceBody[i].speedY += spaceBody[j].Weight * (spaceBody[j].pointY - spaceBody[i].pointY) / R3;
                                }
                            }
                            else if (i > j)
                                Rebound(spaceBody[i], spaceBody[j]);
                        }
                for (int i = 0; i < spaceBody.Count; i++)
                {
                    if (!spaceBody[i].Static)
                    {
                        if (spaceBody[i] is SpaceShip)
                        {
                            SpaceShip ss = (SpaceShip)spaceBody[i];
                            if (ss.RotateLeft) ss.Angle -= 0.1;
                            if (ss.RotateRight) ss.Angle += 0.1;
                            if (ss.Gas)
                            {
                                ss.speedX += Math.Cos(ss.Angle) * 0.002;
                                ss.speedY += Math.Sin(ss.Angle) * 0.002;
                            }
                        }
                        spaceBody[i].pointX += spaceBody[i].speedX;
                        spaceBody[i].pointY += spaceBody[i].speedY;
                        if (LoopCheckBox.Checked)
                        {
                            while (true)
                            {
                                if (spaceBody[i].pointX <= Beginning.X)
                                    spaceBody[i].pointX += pictureBox1.Width;
                                else if (spaceBody[i].pointX >= Beginning.X + pictureBox1.Width)
                                    spaceBody[i].pointX -= pictureBox1.Width;
                                else break;
                            }
                            while (true)
                            {
                                if (spaceBody[i].pointY <= Beginning.Y)
                                    spaceBody[i].pointY += pictureBox1.Height;
                                else if (spaceBody[i].pointY >= Beginning.Y + pictureBox1.Height)
                                    spaceBody[i].pointY -= pictureBox1.Height;
                                else break;
                            }
                        }
                        lock (Lock)
                            if (spaceBody[i].Follow)
                            {
                                Beginning.X = (int)spaceBody[i].pointX - spaceBody[i].OnPicture.X;
                                Beginning.Y = (int)spaceBody[i].pointY - spaceBody[i].OnPicture.Y;
                            }
                        spaceBody[i].Trail.Add(new Point((int)spaceBody[i].pointX, (int)spaceBody[i].pointY));
                        while (spaceBody[i].Trail.Count > LenghtTrail) spaceBody[i].Trail.RemoveAt(0);
                    }
                }
                Thread.Sleep(25 / SpeedTime);
                if (Counter % SpeedTime == 0)
                    DrawSpace();
                if (Counter % 1000 == 0)
                    Save();
                if (Counter >= 1000000)
                    Counter = 0;
            }
        }
        public void DrawSpace()
        {
            if (pictureBox1.Width > 0 && pictureBox1.Height > 0)
                lock (Lock)
                {
                    if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
                    Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    Graphics g = Graphics.FromImage(bmp);
                    //g.DrawImage(DrawGravity(), 0, 0);
                    for (int i = 0; i < spaceBody.Count; i++)
                    {
                        if (spaceBody[i] is SpaceShip)
                        {
                            SpaceShip s = (SpaceShip)spaceBody[i];
                            s.DrawShip();
                            Graphics g1 = Graphics.FromImage(bmp);
                            g1.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
                            g1.RotateTransform((float)s.Angle);
                            g1.TranslateTransform(-pictureBox1.Width / 2, -pictureBox1.Height / 2);
                            g1.DrawImage(s.shipImage, (int)(spaceBody[i].pointX) - Beginning.X - 4,
                                (int)(spaceBody[i].pointY) - Beginning.Y - 3);
                        }
                        else if (!spaceBody[i].Static)
                            g.DrawEllipse(new Pen(spaceBody[i].color), (int)(spaceBody[i].pointX - 2) - Beginning.X,
                                (int)(spaceBody[i].pointY - 2) - Beginning.Y, 4, 4);
                        else
                            g.FillEllipse(new SolidBrush(spaceBody[i].color), (int)(spaceBody[i].pointX - 3) - Beginning.X,
                                (int)(spaceBody[i].pointY - 3) - Beginning.Y, 7, 7);
                        for (int j = 1; j < spaceBody[i].Trail.Count; j++)
                            g.DrawLine(new Pen(spaceBody[i].color, 1), spaceBody[i].Trail[j - 1].X - Beginning.X,
                                spaceBody[i].Trail[j - 1].Y - Beginning.Y, spaceBody[i].Trail[j].X - Beginning.X,
                                spaceBody[i].Trail[j].Y - Beginning.Y);
                        if (!spaceBody[i].Static && SpeedVectorCheckBox.Checked)
                            g.DrawLine(new Pen(Color.Red, 1), (float)spaceBody[i].pointX - Beginning.X,
                                (float)spaceBody[i].pointY - Beginning.Y,
                                (float)spaceBody[i].pointX + (float)spaceBody[i].speedX * 10 - Beginning.X,
                                (float)spaceBody[i].pointY + (float)spaceBody[i].speedY * 10 - Beginning.Y);
                    }
                    pictureBox1.Image = bmp;
                }
        }
        public void AddSpaseBody(double pointX, double pointY, double Weight, bool Static, string Name)
        {
            spaceBody.Add(new SpaceBody(pointX + Beginning.X, pointY + Beginning.Y, Weight, Static, Name));
            SpaceBodyCountLabel.Text = Convert.ToString(spaceBody.Count);
        }
        public void AddSpaseShip(double pointX, double pointY, string Name)
        {
            spaceBody.Add(new SpaceShip(pointX + Beginning.X, pointY + Beginning.Y, Name));
            SpaceBodyCountLabel.Text = Convert.ToString(spaceBody.Count);
        }
        private void Rebound(SpaceBody body1, SpaceBody body2)
        {
            double AngleA = Math.Atan2(body2.pointY - body1.pointY, body2.pointX - body1.pointX);

            double speed1Module = Math.Sqrt(body1.speedX * body1.speedX + body1.speedY * body1.speedY);
            double speed2Module = Math.Sqrt(body2.speedX * body2.speedX + body2.speedY * body2.speedY);
            double speed1Angle = Math.Atan2(body1.speedY, body1.speedX) - AngleA;
            double speed2Angle = Math.Atan2(body2.speedY, body2.speedX) - AngleA;

            body1.speedX = speed1Module * Math.Cos(speed1Angle);
            body1.speedY = speed1Module * Math.Sin(speed1Angle);
            body2.speedX = speed2Module * Math.Cos(speed2Angle);
            body2.speedY = speed2Module * Math.Sin(speed2Angle);

            double t = body1.speedX * body1.Weight / body2.Weight;
            body1.speedX = body2.speedX * body2.Weight / body1.Weight;
            body2.speedX = t;

            speed1Module = Math.Sqrt(body1.speedX * body1.speedX + body1.speedY * body1.speedY);
            speed2Module = Math.Sqrt(body2.speedX * body2.speedX + body2.speedY * body2.speedY);
            speed1Angle = Math.Atan2(body1.speedY, body1.speedX) + AngleA;
            speed2Angle = Math.Atan2(body2.speedY, body2.speedX) + AngleA;

            body1.speedX = speed1Module * Math.Cos(speed1Angle);
            body1.speedY = speed1Module * Math.Sin(speed1Angle);
            body2.speedX = speed2Module * Math.Cos(speed2Angle);
            body2.speedY = speed2Module * Math.Sin(speed2Angle);
        }
        public void Orbit(SpaceBody SunBody, SpaceBody PlanetBody, bool clockwise)
        {
            PlanetBody.speedX = SunBody.speedX;
            PlanetBody.speedY = SunBody.speedY;
            double R = Math.Sqrt((PlanetBody.pointX - SunBody.pointX) * (PlanetBody.pointX - SunBody.pointX) +
                (PlanetBody.pointY - SunBody.pointY) * (PlanetBody.pointY - SunBody.pointY));
            double AngleA = Math.Atan2(PlanetBody.pointY - SunBody.pointY, PlanetBody.pointX - SunBody.pointX);
            if (clockwise) AngleA += Math.PI / 2;
            else AngleA -= Math.PI / 2;
            PlanetBody.speedX += Math.Sqrt(SunBody.Weight / R) * Math.Cos(AngleA);
            PlanetBody.speedY += Math.Sqrt(SunBody.Weight / R) * Math.Sin(AngleA);
        }
        private void Save()
        {
            string Save = "";
            for (int i = 0; i < spaceBody.Count; i++)
            {
                if (i != 0)
                    Save += "%";
                Save += spaceBody[i].pointX + "#";
                Save += spaceBody[i].pointY + "#";
                Save += spaceBody[i].speedX + "#";
                Save += spaceBody[i].speedY + "#";
                Save += spaceBody[i].Weight + "#";
                if (spaceBody[i] is SpaceShip)
                    Save += "2#";
                else if (spaceBody[i].Static)
                    Save += "1#";
                else
                    Save += "0#";
                Save += spaceBody[i].Name + "#";
                Save += spaceBody[i].color.ToArgb();
                if (spaceBody[i] is SpaceShip)
                {
                    SpaceShip ss = (SpaceShip)spaceBody[i];
                    Save += "#" + ss.Angle;
                }
            }
            Settings.Default["SaveFile"] = Save;
            Settings.Default.Save();
        }
        private Bitmap DrawGravity()
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            double[,] gravityField = new double[bmp.Width / 10 + 1, bmp.Height / 10 + 1];
            for (int i = 0; i < gravityField.GetLength(0); i++)
                for (int j = 0; j < gravityField.GetLength(1); j++)
                {
                    double dX = 0, dY = 0;
                    for (int k = 0; k < spaceBody.Count; k++)
                    {
                        double R3 = Math.Pow((spaceBody[k].pointX - Beginning.X - 10 * i - 5) * (spaceBody[k].pointX - Beginning.X - 10 * i - 5) +
                                (spaceBody[k].pointY - Beginning.Y - 10 * j - 5) * (spaceBody[k].pointY - Beginning.Y - 10 * j - 5), 3.0 / 2);
                        dX += spaceBody[k].Weight * (spaceBody[k].pointX - Beginning.X - 10 * i - 5) / R3;
                        dY += spaceBody[k].Weight * (spaceBody[k].pointY - Beginning.Y - 10 * j - 5) / R3;
                    }
                    gravityField[i, j] = Kill(Math.Sqrt(dX * dX + dY * dY));
                    g.FillRectangle(new SolidBrush(Color.FromArgb((byte)gravityField[i, j], 0, 0)), i * 10, j * 10, 10, 10);
                }
            return bmp;
        }
        double Kill(double x)
        {
            return 255 / (1 + Math.Pow(Math.E, -(x - 0.02) * 100));
        }


        public FormMain()
        {
            InitializeComponent();
            t1 = new Thread(Simulation);
            try
            {
                string Save = Settings.Default["SaveFile"].ToString();
                for (int i = 0; i < Save.Split('%').Length; i++)
                {
                    if (Save.Split('%')[i].Split('#')[5] == "1")
                        spaceBody.Add(new SpaceBody(Convert.ToDouble(Save.Split('%')[i].Split('#')[0]),
                            Convert.ToDouble(Save.Split('%')[i].Split('#')[1]), Convert.ToDouble(Save.Split('%')[i].Split('#')[4]),
                            true, Save.Split('%')[i].Split('#')[6]));
                    else if (Save.Split('%')[i].Split('#')[5] == "0")
                        spaceBody.Add(new SpaceBody(Convert.ToDouble(Save.Split('%')[i].Split('#')[0]),
                        Convert.ToDouble(Save.Split('%')[i].Split('#')[1]), Convert.ToDouble(Save.Split('%')[i].Split('#')[4]),
                        false, Save.Split('%')[i].Split('#')[6]));
                    else if (Save.Split('%')[i].Split('#')[5] == "2")
                    {
                        spaceBody.Add(new SpaceShip(Convert.ToDouble(Save.Split('%')[i].Split('#')[0]),
                        Convert.ToDouble(Save.Split('%')[i].Split('#')[1]), Save.Split('%')[i].Split('#')[6]));
                        SpaceShip ss = (SpaceShip)spaceBody[i];
                        ss.Angle = Convert.ToDouble(Save.Split('%')[i].Split('#')[8]);
                    }
                    spaceBody[i].speedX = Convert.ToDouble(Save.Split('%')[i].Split('#')[2]);
                    spaceBody[i].speedY = Convert.ToDouble(Save.Split('%')[i].Split('#')[3]);
                    spaceBody[i].color = Color.FromArgb(Convert.ToInt32(Save.Split('%')[i].Split('#')[7]));
                }
            }
            catch
            {
            }
            DrawSpace();
        }
        private void StartStopButton_Click(object sender, EventArgs e)
        {
            if (Stop)
            {
                Stop = false;
                StartStopButton.Text = "Stop";
                try
                {
                    t1.Start();
                }
                catch
                {
                    t1.Abort();
                    t1 = new Thread(Simulation);
                    t1.Start();
                }
            }
            else
            {
                Stop = true;
                StartStopButton.Text = "Start";
                Thread.Sleep(60);
                LenghtTrail = LenghtTrackBar.Value;
                SpeedTime = SpeedTrackBar.Value;
            }
            Save();
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!Stop)
            {
                Stop = true;
                StartStopButton.Text = "Start";
                Thread.Sleep(60);
            }
            FormAddBody form = new FormAddBody(this);
            form.ShowDialog();
            DrawSpace();
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            Stop = true;
            Thread.Sleep(60);
            spaceBody.Clear();
            StartStopButton.Text = "Start";
            SpaceBodyCountLabel.Text = Convert.ToString(spaceBody.Count);
            Beginning = new Point(0, 0);
            DrawSpace();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (t1 != null) t1.Abort();
            Save();
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (Stop) DrawSpace();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            MousDownCheck = true;
            MousDownPoint = e.Location;
            previousPoint = MousDownPoint;
            if (Stop)
            {
                MousButtonLeft = (e.Button == MouseButtons.Left);
                for (int i = 0; i < spaceBody.Count; i++)
                {
                    if (e.Location.X - spaceBody[i].pointX + Beginning.X > -5 && e.Location.X - spaceBody[i].pointX + Beginning.X < 5 &&
                        e.Location.Y - spaceBody[i].pointY + Beginning.Y > -5 && e.Location.Y - spaceBody[i].pointY + Beginning.Y < 5)
                    {
                        CurrentBody = spaceBody[i];
                        break;
                    }
                }
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (MousDownCheck && CurrentBody != null)
            {
                if (MousButtonLeft)
                {
                    CurrentBody.pointX += e.Location.X - previousPoint.X;
                    CurrentBody.pointY += e.Location.Y - previousPoint.Y;
                    previousPoint = e.Location;
                }
                else
                {
                    CurrentBody.speedX = (e.Location.X + Beginning.X - CurrentBody.pointX) / 10;
                    CurrentBody.speedY = (e.Location.Y + Beginning.Y - CurrentBody.pointY) / 10;
                }
                DrawSpace();
            }
            else if (MousDownCheck && CurrentBody == null)
            {
                lock (Lock)
                {
                    Beginning.X -= e.Location.X - previousPoint.X;
                    Beginning.Y -= e.Location.Y - previousPoint.Y;
                    for (int i = 0; i < spaceBody.Count; i++)
                        if (spaceBody[i].Follow)
                        {
                            spaceBody[i].OnPicture.X += e.Location.X - previousPoint.X;
                            spaceBody[i].OnPicture.Y += e.Location.Y - previousPoint.Y;
                            break;
                        }
                }
                previousPoint = e.Location;
                if (Stop)
                    DrawSpace();
            }
            LabelPointX.Text = Convert.ToString(e.X + Beginning.X);
            LabelPointY.Text = Convert.ToString(e.Y + Beginning.Y);
            SpaceBodyCountLabel.Text = Convert.ToString(spaceBody.Count);
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            MousDownCheck = false;
            if (e.Location == MousDownPoint && Stop && CurrentBody != null)
            {
                FormChangeBody form = new FormChangeBody(this, CurrentBody);
                form.ShowDialog();
            }
            CurrentBody = null;
            SpaceBodyCountLabel.Text = Convert.ToString(spaceBody.Count);
            DrawSpace();
        }
        private void LenghtTrackBar_Scroll(object sender, EventArgs e)
        {
            LenghtLabel.Text = Convert.ToString(LenghtTrackBar.Value);
            if (Stop) LenghtTrail = LenghtTrackBar.Value;
        }
        private void AdditionallyButton_Click(object sender, EventArgs e)
        {
            if (!Stop)
            {
                Stop = true;
                StartStopButton.Text = "Start";
                Thread.Sleep(60);
            }
            FormEveryBody form = new FormEveryBody(this);
            form.ShowDialog();
        }
        private void SpeedVectorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Stop)
                DrawSpace();
        }
        private void SpeedTrackBar_Scroll(object sender, EventArgs e)
        {
            if (Stop) SpeedTime = SpeedTrackBar.Value;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Stop)
            {
                SpaceShip ss = null;
                for (int i = 0; i < spaceBody.Count; i++)
                    if (spaceBody[i] is SpaceShip)
                    {
                        ss = (SpaceShip)spaceBody[i];
                        if (ss.Driving)
                            break;
                        else
                            ss = null;
                    }
                if (ss != null)
                {
                    if (e.KeyCode == Keys.W) ss.Gas = true;
                    if (e.KeyCode == Keys.A) ss.RotateLeft = true;
                    if (e.KeyCode == Keys.D) ss.RotateRight = true;
                }
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (!Stop)
            {
                SpaceShip ss = null;
                for (int i = 0; i < spaceBody.Count; i++)
                    if (spaceBody[i] is SpaceShip)
                    {
                        ss = (SpaceShip)spaceBody[i];
                        if (ss.Driving)
                            break;
                        else
                            ss = null;
                    }
                if (ss != null)
                {
                    if (e.KeyCode == Keys.W) ss.Gas = false;
                    if (e.KeyCode == Keys.A) ss.RotateLeft = false;
                    if (e.KeyCode == Keys.D) ss.RotateRight = false;
                }
            }
        }
    }
    public class SpaceBody
    {
        public double pointX = 0, pointY = 0;
        public double speedX = 0, speedY = 0;
        public double Weight = 0;
        public bool Static = false;
        public bool Follow = false;
        public Point OnPicture;
        public List<Point> Trail = new List<Point>();
        public string Name;
        public Color color = Color.White;
        public SpaceBody(double pointX, double pointY, double Weight, bool Static, string Name)
        {
            this.pointX = pointX;
            this.pointY = pointY;
            this.Weight = Weight;
            this.Static = Static;
            this.Name = Name;
        }
        public SpaceBody()
        {

        }
    }
    public class SpaceShip : SpaceBody
    {
        public bool Driving = false;
        public bool Gas = false;
        public bool RotateRight = false;
        public bool RotateLeft = false;
        public double Angle = 0;
        public Bitmap shipImage = new Bitmap(9, 7);
        public SpaceShip(double pointX, double pointY, string Name)
        {
            this.pointX = pointX;
            this.pointY = pointY;
            this.Name = Name;
            DrawShip();
        }
        public void DrawShip()
        {
            Graphics g = Graphics.FromImage(shipImage);
            g.DrawLine(new Pen(color, 1), 0, 0, 8, 3);
            g.DrawLine(new Pen(color, 1), 0, 6, 8, 3);
            g.DrawLine(new Pen(color, 1), 0, 0, 2, 3);
            g.DrawLine(new Pen(color, 1), 0, 6, 2, 3);
        }
        public Bitmap rotateImage()
        {
            Bitmap result = new Bitmap(shipImage.Width, shipImage.Height);
            Graphics g = Graphics.FromImage(result);
            g.TranslateTransform((float)shipImage.Width / 2, (float)shipImage.Height / 2);
            g.RotateTransform((float)Angle);
            g.TranslateTransform(-(float)shipImage.Width / 2, -(float)shipImage.Height / 2);
            g.DrawImage(shipImage, new Point(0, 0));
            return result;
        }
    }
}