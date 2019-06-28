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
        public int SpeedTime { get; private set; } = 25;
        private Action<Bitmap> graphics;
        private Func<Size> getSize;
        private object Lock = new object();
        private bool speedVector = true;
        public bool SpeedVector
        {
            get { return speedVector; }
            set { lock (Lock) speedVector = value; }
        }

        public SimulationSpace(Action<Bitmap> graphics, Func<Size> getSize)
        {
            this.graphics = graphics;
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
                    b.point.X += p.X;
                    b.point.Y += p.Y;
                }
        }
        public SpaceBody FindBody(Point coordinate)
        {
            if (!Play)
                return space.Find(coordinate);
            else return null;
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
                    b.speed.X = (p.X + space.Beginning.X - b.point.X) / (float)16.0;
                    b.speed.Y = (p.Y + space.Beginning.Y - b.point.Y) / (float)16.0;
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
                SpaceBody body = new SpaceStaticBody(point, Weight, Name);
                space.AddBody(body);
            }
        }
        public void AddShip(PointF point, string Name)
        {
            if (!Play)
            {
                point.X += space.Beginning.X;
                point.Y += space.Beginning.Y;
                SpaceBody body = new SpaceShip(point, Name);
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
        public void SpeedSimulation(int speed)
        {
            SpeedTime = speed;
        }
        public void NewBackground(ISpaceBackground b)
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
                Thread.Sleep(SpeedTime);
            }
            Thread t = threadSpace;
            threadSpace = null;
            t.Abort();
        }
        private void FPS()
        {
            Bitmap bmp;
            while (true)
            {
                Size t = getSize();
                lock (Lock)
                    bmp = space.GetPicture(t.Width, t.Height, SpeedVector);
                graphics(bmp);
                Thread.Sleep(10);
                GC.Collect();
            }
        }
    }
}
