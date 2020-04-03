using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace орбитальная_механика
{
    public class SimulationSpace
    {
        private Space space = new Space();
        public bool Play { get; private set; } = false;
        private Thread threadSpace;
        private Thread threadGraphics;
        public double SpeedTime { get; private set; } = 10;
        private double SpeedTimeCheck = 0;
        private FormMain form;
        private Func<Size> getSize;
        private object Lock = new object();
        private bool speedVector = true;
        private bool radar = true;
        public bool SpeedVector
        {
            get { return speedVector; }
            set { lock (Lock) speedVector = value; }
        }
        public bool Radar
        {
            get { return radar; }
            set { lock (Lock) radar = value; }
        }

        public SimulationSpace(FormMain form, Func<Size> getSize)
        {
            this.form = form;
            this.getSize = getSize;
            threadGraphics = new Thread(FPS);
            threadGraphics.Start();
        }
        ~SimulationSpace()
        {
            if (threadGraphics != null) threadGraphics.Abort();
            if (threadSpace != null) threadSpace.Abort();
        }

        public void Start()
        {
            if (!Play)
            {
                Play = true;
                threadSpace = new Thread(Simulation);
                threadSpace.Start();
            }
        }
        public void Stop()
        {
            if (Play)
            {
                Play = false;
                threadSpace.Join();
            }
        }
        public void MoveSpace(Point p)
        {
            space.MoveSpace(p);
        }
        public void MoveBody(SpaceBody b, Point p)
        {
            if (b != null && !Play)
                lock (Lock)
                {
                    b.X += p.X;
                    b.Y += p.Y;
                }
        }
        public SpaceBody FindBody(Point coordinate)
        {
            if (!Play)
                return space.Find(coordinate);
            else return null;
        }
        public SpaceBody FindBody(string Name)
        {
            return space.Find(Name);
        }
        public SpaceBody[] AllBodies()
        {
            return space.bodies.ToArray();
        }
        public Space GetSpace()
        {
            return space;
        }
        public void ChangeSpeedVector(SpaceBody b, Point p)
        {
            if (!Play)
                lock (Lock)
                {
                    b.sX = (p.X + space.Beginning.X - b.X) / (float)16.0;
                    b.sY = (p.Y + space.Beginning.Y - b.Y) / (float)16.0;
                }
        }
        public void AddBody(PointF point, float Weight, string Name)
        {
            if (!Play)
            {
                point.X += space.Beginning.X;
                point.Y += space.Beginning.Y;
                SpaceBody body = new SpaceBody(point, Weight, Name);
                space.AddBody(body);
            }
        }
        public void AddBody(SpaceBody body)
        {
            if (!Play)
                space.AddBody(body);
        }
        public void AddStaticBody(PointF point, float Weight, string Name)
        {
            if (!Play)
            {
                point.X += space.Beginning.X;
                point.Y += space.Beginning.Y;
                SpaceStaticBody body = new SpaceStaticBody(point, Weight, Name);
                space.AddBody(body);
            }
        }
        public void AddShip(PointF point, string Name)
        {
            if (!Play)
            {
                point.X += space.Beginning.X;
                point.Y += space.Beginning.Y;
                SpaceShip body = new SpaceShip(point, Name);
                space.AddBody(body);
            }
        }
        public void DeleteBody(SpaceBody b)
        {
            space.RemoveBody(b);
        }
        public void Orbit(SpaceBody Main, SpaceBody Satellite, bool clockwise)
        {
            space.Orbit(Main, Satellite, clockwise);
        }
        public void Clear()
        {
            Play = false;
            space.Clear();
        }
        public void SpeedSimulation(double speed)
        {
            SpeedTime = speed;
        }
        public void NewBackground(IBackground b)
        {
            space.NewBackground(b);
        }
        public void Delete()
        {
            if (threadGraphics != null) threadGraphics.Abort();
            if (threadSpace != null) threadSpace.Abort();
        }

        private void Simulation()
        {
            while (Play)
            {
                space.Teak();

                double timeSpeedTime = SpeedTime * space.Slow;
                if (timeSpeedTime <= 1)
                {
                    SpeedTimeCheck += timeSpeedTime;
                    if (SpeedTimeCheck >= 1)
                    {
                        Thread.Sleep(1);
                        SpeedTimeCheck -= 1;
                    }
                }
                else Thread.Sleep((int)timeSpeedTime);
            }
        }
        private void FPS()
        {
            Bitmap bmp;
            while (true)
            {
                Size t = getSize();
                lock (Lock)
                    bmp = space.GetPicture(t.Width, t.Height, SpeedVector, Radar);
                form.Invoke((Action)(() => form.pictureBox1.Image = bmp));
                GC.Collect();
            }
        }
    }
}
