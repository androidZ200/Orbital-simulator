using System;
using System.Collections.Generic;
using System.Drawing;

namespace орбитальная_механика
{
    public class Space
    {
        public List<SpaceObject> bodies { get; set; } = new List<SpaceObject>();
        public SpaceObject follow { get; set; }
        public int LengthTrail
        {
            get
            {
                return SpaceBody.MaxLengthTrail;
            }
            set
            {
                SpaceBody.MaxLengthTrail = value;
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
        public float Slow { get; private set; } = 1;

        private PointF beginning = new PointF(0, 0);
        private object MoveLockBody = new object();
        private IBackground background = new SpaceBackgroundBlack();

        public void AddBody(SpaceObject body)
        {
            lock (MoveLockBody)
                bodies.Add(body);
        }
        public void RemoveBody(SpaceObject body)
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
                    float s = 0;
                    if (bodies[j] is SpaceBody)
                        s = bodies[i].SpeedCorrection((SpaceBody)bodies[j], Slow);
                    if (s > t) t = s;
                }
            lock (MoveLockBody)
                for (int i = 0; i < bodies.Count; i++)
                    bodies[i].Move(Slow);
            if (follow != null)
                lock (MoveLockBody)
                {
                    beginning.X += follow.speed.X * Slow;
                    beginning.Y += follow.speed.Y * Slow;
                }

            if (t * Slow > 0.1) Slow /= 2;
            else Slow *= 2;
            if (Slow > 1) Slow = 1;
        }
        public void MoveSpace(Point pixels)
        {
            lock (MoveLockBody)
            {
                beginning.X += pixels.X;
                beginning.Y += pixels.Y;
            }
        }
        public Bitmap GetPicture(int width, int height, bool vectorSpeed, bool radar)
        {
            if (width > 0 && height > 0)
            {
                Bitmap bmp = background.GetBackground(new Point(-(int)beginning.X, -(int)beginning.Y), width, height);
                Graphics g = Graphics.FromImage(bmp);

                lock (MoveLockBody)
                {
                    for (int i = 0; i < bodies.Count; i++)
                        if (bodies[i] is SpaceBody)
                        {
                            SpaceBody sb = (SpaceBody)bodies[i];
                            var s = sb.Trail.GetEnumerator();
                            s.MoveNext();
                            var prev = s;
                            while (s.MoveNext())
                            {
                                g.DrawLine(new Pen(sb.color, 1), prev.Current.X - beginning.X,
                                    prev.Current.Y - beginning.Y, s.Current.X - beginning.X, s.Current.Y - beginning.Y);
                                prev = s;
                            }
                        }
                    for (int i = 0; i < bodies.Count; i++)
                        if (bodies[i].point.X >= beginning.X - 6 && bodies[i].point.X <= beginning.X + width + 6 &&
                            bodies[i].point.Y >= beginning.Y - 6 && bodies[i].point.Y <= beginning.Y + height + 6)
                        {
                            if (vectorSpeed)
                                g.DrawLine(new Pen(Color.Red, 1), bodies[i].point.X - beginning.X, bodies[i].point.Y - beginning.Y,
                                    bodies[i].point.X - beginning.X + bodies[i].speed.X * 16,
                                    bodies[i].point.Y - beginning.Y + bodies[i].speed.Y * 16);
                            g.DrawImage(bodies[i].GetPicture(), bodies[i].point.X - beginning.X - 6, bodies[i].point.Y - beginning.Y - 6);
                        }
                    if (radar) DrawRadar(width, height, g);
                }
                return bmp;
            }
            return null;
        }
        public void DrawRadar(int width, int height, Graphics g)
        {
            g.DrawEllipse(new Pen(Color.MediumBlue), 15, 15, width - 30, height - 30);
            g.DrawEllipse(new Pen(Color.DarkBlue), 5, 5, width - 10, height - 10);
            g.DrawEllipse(new Pen(Color.RoyalBlue), -60, -60, width + 120, height + 120);

            for (int i = 0; i < bodies.Count; i++)
            {
                if (!(bodies[i].point.X >= beginning.X - 6 && bodies[i].point.X <= beginning.X + width + 6 &&
                            bodies[i].point.Y >= beginning.Y - 6 && bodies[i].point.Y <= beginning.Y + height + 6))
                {
                    PointF center = new PointF(beginning.X + width / 2f, beginning.Y + height / 2f);
                    float a = width / 2 - 10;
                    float b = height / 2 - 10;
                    double ctg = (bodies[i].point.X - center.X) / (bodies[i].point.Y - center.Y);
                    float Y = (float)Math.Sqrt((a * a * b * b) / (b * b * ctg * ctg + a * a));
                    if (bodies[i].point.Y - center.Y < 0) Y = -Y;
                    float X;
                    if (Y != 0)
                        X = (float)(Y * ctg);
                    else
                        X = bodies[i].point.X - center.X > 0 ? a : -a;

                    g.DrawImage(bodies[i].GetPicture(), X + a + 4, Y + b + 4);
                }
            }
        }
        public void Orbit(SpaceBody SunBody, SpaceObject PlanetBody, bool clockwise)
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
        public SpaceObject Find(PointF coordinate)
        {
            for (int i = 0; i < bodies.Count; i++)
                if (coordinate.X - bodies[i].point.X + beginning.X > -5 && coordinate.X - bodies[i].point.X + beginning.X < 5 &&
                    coordinate.Y - bodies[i].point.Y + beginning.Y > -5 && coordinate.Y - bodies[i].point.Y + beginning.Y < 5)
                    return bodies[i];
            return null;
        }
        public SpaceObject Find(string Name)
        {
            return bodies.Find((x) => Name == x.Name);
        }
        public void Clear()
        {
            lock (MoveLockBody)
                bodies.Clear();
            follow = null;
            beginning = Point.Empty;
        }
        public void NewBackground(IBackground b)
        {
            lock (MoveLockBody)
                background = b;
        }
    }
}
