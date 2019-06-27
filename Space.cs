using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace орбитальная_механика
{
    public class Space
    {
        public List<SpaceBody> bodies { get; set; } = new List<SpaceBody>();
        public SpaceBody follow { get; set; }
        public int LengthTrail
        {
            get
            {
                return lengthTrail;
            }
            set
            {
                lengthTrail = value;
                lock (MoveLockBody)
                    for (int i = 0; i < bodies.Count; i++)
                        bodies[i].MaxLengthTrail = lengthTrail;
            }
        }
        public PointF Beginning
        {
            get
            {
                return beginning;
            }
            set
            {
                lock (MoveLockBody)
                    beginning = value;
            }
        }

        private int lengthTrail = 50;
        private PointF beginning = new PointF(0, 0);
        private object MoveLockBody = new object();
        private float slow = 1;
        private SpaceBackground background = new SpaceBackground();

        public void AddBody(SpaceBody body)
        {
            body.MaxLengthTrail = LengthTrail;
            lock (MoveLockBody)
                bodies.Add(body);
        }
        public void RemoveBody(SpaceBody body)
        {
            lock (MoveLockBody)
                bodies.Remove(body);
        }
        public void Teak()
        {
            float t = 0;
            for (int i = 0; i < bodies.Count; i++)
                for (int j = i + 1; j < bodies.Count; j++)
                {
                    float s = bodies[i].SpeedCorrection(bodies[j], slow);
                    if (s > t) t = s;
                }
            lock (MoveLockBody)
                for (int i = 0; i < bodies.Count; i++)
                {
                    if (bodies[i] is SpaceShip)
                    {
                        ((SpaceShip)(bodies[i])).Fly(slow);
                        ((SpaceShip)(bodies[i])).Rotate(slow);
                    }
                    bodies[i].Move(slow);
                }
            if (follow != null)
                lock (MoveLockBody)
                {
                    beginning.X += follow.speed.X * slow;
                    beginning.Y += follow.speed.Y * slow;
                }

            if (t * slow > 0.1) slow /= 2;
            else slow *= 2;
            if (slow > 1) slow = 1;
        }
        public void MoveSpace(Point pixels)
        {
            lock (MoveLockBody)
            {
                beginning.X += pixels.X;
                beginning.Y += pixels.Y;
            }
        }
        public Bitmap GetPicture(int width, int height, bool vectorSpeed)
        {
            if (width > 0 && height > 0)
            {
                Bitmap bmp = new Bitmap(width, height);
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.Black);
                g.DrawImage(background.GetBackground(new Point(-(int)beginning.X, -(int)beginning.Y)), 0, 0);

                lock (MoveLockBody)
                {
                    for (int i = 0; i < bodies.Count; i++)
                    {
                        var s = bodies[i].Trail.GetEnumerator();
                        s.MoveNext();
                        var prev = s;
                        while (s.MoveNext())
                        {
                            g.DrawLine(new Pen(bodies[i].color, 1), prev.Current.X - beginning.X,
                                prev.Current.Y - beginning.Y, s.Current.X - beginning.X, s.Current.Y - beginning.Y);
                            prev = s;
                        }
                    }
                    for (int i = 0; i < bodies.Count; i++)
                    {
                        if (vectorSpeed)
                            g.DrawLine(new Pen(Color.Red, 1), bodies[i].point.X - beginning.X, bodies[i].point.Y - beginning.Y,
                                bodies[i].point.X - beginning.X + bodies[i].speed.X * 4,
                                bodies[i].point.Y - beginning.Y + bodies[i].speed.Y * 4);
                        g.DrawImage(bodies[i].GetPicture(), bodies[i].point.X - beginning.X - 6, bodies[i].point.Y - beginning.Y - 6);
                    }
                }
                return bmp;
            }
            return null;
        }
        public void Orbit(SpaceBody SunBody, SpaceBody PlanetBody, bool clockwise)
        {
            PlanetBody.speed.X = SunBody.speed.X;
            PlanetBody.speed.Y = SunBody.speed.Y;
            double R = Math.Sqrt((PlanetBody.point.X - SunBody.point.X) * (PlanetBody.point.X - SunBody.point.X) +
                (PlanetBody.point.Y - SunBody.point.Y) * (PlanetBody.point.Y - SunBody.point.Y));
            double AngleA = Math.Atan2(PlanetBody.point.Y - SunBody.point.Y, PlanetBody.point.X - SunBody.point.X);
            if (clockwise) AngleA += Math.PI / 2;
            else AngleA -= Math.PI / 2;
            PlanetBody.speed.X += (float)(Math.Sqrt(SunBody.Weight / R) * Math.Cos(AngleA));
            PlanetBody.speed.Y += (float)(Math.Sqrt(SunBody.Weight / R) * Math.Sin(AngleA));
        }
        public SpaceBody Find(PointF coordinate)
        {
            for (int i = 0; i < bodies.Count; i++)
                if (coordinate.X - bodies[i].point.X + beginning.X > -5 && coordinate.X - bodies[i].point.X + beginning.X < 5 &&
                    coordinate.Y - bodies[i].point.Y + beginning.Y > -5 && coordinate.Y - bodies[i].point.Y + beginning.Y < 5)
                    return bodies[i];
            return null;
        }
        public void Clear()
        {
            lock (MoveLockBody)
                bodies.Clear();
            follow = null;
            beginning = Point.Empty;
        }
    }
}
