using System;
using System.Collections.Generic;
using System.Drawing;

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
                return SpaceBody.MaxLengthTrail;
            }
            set
            {
                SpaceBody.MaxLengthTrail = value;
                foreach (var x in bodies) lock (x) x.ChangetLengthTrail();
            }
        }
        public int LengthFuture
        {
            get
            {
                return SpaceBody.LengthFuture;
            }
            set
            {
                SpaceBody.LengthFuture = value + 1;
                foreach (var x in bodies) lock (x) x.ChangetLengthFuture();
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
                beginning = value;
            }
        }
        public double Slow
        {
            get { return historySlow[0]; }
        }

        private List<float> historySlow = new List<float>();
        private PointF beginning = new PointF();
        private IBackground background;

        public Space()
        {
            historySlow.Add(1);
            background = new SpaceBackgroundStaticRetro();
        }

        public void AddBody(SpaceBody body)
        {
            ResetFuture();
            lock (bodies)
                bodies.Add(body);
        }
        public void RemoveBody(SpaceBody body)
        {
            ResetFuture();
            lock (bodies)
                bodies.Remove(body);
        }
        public void Teak()
        {
            FutureTeak();
            if (follow != null)
                lock (follow)
                {
                    beginning.X += follow.sX * historySlow[0];
                    beginning.Y += follow.sY * historySlow[0];
                }
            foreach (var x in bodies)
                lock (x)
                {
                    x.Move();
                    if (x is SpaceShip) ((SpaceShip)x).MoveGas(historySlow[0]);
                }
            historySlow.RemoveAt(0);

        }
        public void MoveSpace(Point pixels)
        {
            beginning.X += pixels.X;
            beginning.Y += pixels.Y;
        }
        public Bitmap GetPicture(int width, int height, bool vectorSpeed, bool radar)
        {
            if (width > 0 && height > 0)
            {
                Bitmap bmp;
                bmp = background.GetBackground(new Point(-(int)beginning.X, -(int)beginning.Y), width, height);
                Graphics g = Graphics.FromImage(bmp);

                lock (bodies)
                    for (int i = 0; i < bodies.Count; i++)
                    {
                        Pen p = new Pen(bodies[i].color, 1);
                        lock (bodies[i])
                        {
                            var s = bodies[i].Trail.GetEnumerator();
                            s.MoveNext();
                            var prev = s;
                            while (s.MoveNext())
                            {
                                if (prev.Current != null)
                                    g.DrawLine(p, prev.Current.X - beginning.X,
                                        prev.Current.Y - beginning.Y, s.Current.X - beginning.X, s.Current.Y - beginning.Y);
                                prev = s;
                            }
                        }
                        lock (bodies[i])
                        {
                            var s = bodies[i].Future.GetEnumerator();
                            s.MoveNext();
                            var prev = s;
                            while (s.MoveNext())
                            {
                                if (prev.Current != null)
                                    g.DrawLine(p, prev.Current.point.X - beginning.X,
                                        prev.Current.point.Y - beginning.Y, s.Current.point.X - beginning.X, s.Current.point.Y - beginning.Y);
                                prev = s;
                            }
                        }
                    }
                for (int i = 0; i < bodies.Count; i++)
                    lock (bodies[i])
                        if (bodies[i].X >= beginning.X - 6 && bodies[i].X <= beginning.X + width + 6 &&
                        bodies[i].Y >= beginning.Y - 6 && bodies[i].Y <= beginning.Y + height + 6)
                        {
                            if (vectorSpeed)
                                g.DrawLine(new Pen(Color.Red, 1), bodies[i].X - beginning.X, bodies[i].Y - beginning.Y,
                                bodies[i].X - beginning.X + bodies[i].speed.X * 16,
                                bodies[i].Y - beginning.Y + bodies[i].speed.Y * 16);
                            bodies[i].GetPicture(g, new Point((int)(bodies[i].X - beginning.X), (int)(bodies[i].Y - beginning.Y)));
                        }
                if (radar) DrawRadar(width, height, g);
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
                lock (bodies[i])
                    if (!(bodies[i].X >= beginning.X - 6 && bodies[i].X <= beginning.X + width + 6 &&
                            bodies[i].Y >= beginning.Y - 6 && bodies[i].Y <= beginning.Y + height + 6))
                    {
                        PointF center = new PointF(beginning.X + width / 2f, beginning.Y + height / 2f);
                        float a = width / 2 - 10;
                        float b = height / 2 - 10;
                        double ctg = (bodies[i].X - center.X) / (bodies[i].Y - center.Y);
                        float Y = (float)Math.Sqrt((a * a * b * b) / (b * b * ctg * ctg + a * a));
                        if (bodies[i].Y - center.Y < 0) Y = -Y;
                        float X;
                        if (Y != 0)
                            X = (float)(Y * ctg);
                        else
                            X = bodies[i].X - center.X > 0 ? a : -a;

                        bodies[i].GetPicture(g, new Point((int)(X + a + 10), (int)(Y + b + 10)));
                    }
        }
        public void Orbit(SpaceBody SunBody, SpaceBody PlanetBody, bool clockwise)
        {
            PointF ImpulseSystem = PointF.Empty;
            ImpulseSystem.X += SunBody.Weight * SunBody.sX + PlanetBody.Weight * PlanetBody.sX;
            ImpulseSystem.Y += SunBody.Weight * SunBody.sY + PlanetBody.Weight * PlanetBody.sY;

            double R = Math.Sqrt((PlanetBody.X - SunBody.X) * (PlanetBody.X - SunBody.X) +
                (PlanetBody.Y - SunBody.Y) * (PlanetBody.Y - SunBody.Y));
            if (R <= 4d) throw new Exception();
            PlanetBody.sX = SunBody.sX;
            PlanetBody.sY = SunBody.sY;
            double AngleA = Math.Atan2(PlanetBody.Y - SunBody.Y, PlanetBody.X - SunBody.X);
            if (clockwise) AngleA += Math.PI / 2;
            else AngleA -= Math.PI / 2;
            PlanetBody.sX += (float)(Math.Sqrt(SunBody.Weight / R) * Math.Cos(AngleA));
            PlanetBody.sY += (float)(Math.Sqrt(SunBody.Weight / R) * Math.Sin(AngleA));

            ImpulseSystem.X -= SunBody.Weight * SunBody.sX + PlanetBody.Weight * PlanetBody.sX;
            ImpulseSystem.Y -= SunBody.Weight * SunBody.sY + PlanetBody.Weight * PlanetBody.sY;
            SunBody.sX += ImpulseSystem.X / (PlanetBody.Weight + SunBody.Weight);
            SunBody.sY += ImpulseSystem.Y / (PlanetBody.Weight + SunBody.Weight);
            PlanetBody.sX += ImpulseSystem.X / (PlanetBody.Weight + SunBody.Weight);
            PlanetBody.sY += ImpulseSystem.Y / (PlanetBody.Weight + SunBody.Weight);
        }
        public SpaceBody Find(PointF coordinate)
        {
            lock (bodies)
                for (int i = 0; i < bodies.Count; i++)
                    if (coordinate.X - bodies[i].X + beginning.X > -5 && coordinate.X - bodies[i].X + beginning.X < 5 &&
                        coordinate.Y - bodies[i].Y + beginning.Y > -5 && coordinate.Y - bodies[i].Y + beginning.Y < 5)
                        return bodies[i];
            return null;
        }
        public SpaceBody Find(string Name)
        {
            return bodies.Find((x) => Name == x.Name);
        }
        public void Clear()
        {
            lock (bodies)
                bodies.Clear();
            follow = null;
            beginning = Point.Empty;
        }
        public void NewBackground(IBackground b)
        {
            background = b;
        }

        private void ResetFuture()
        {
            foreach (var x in bodies)
                lock (x)
                    x.Changet();
            float t = historySlow[0];
            historySlow.Clear();
            historySlow.Add(t);
        }
        private void FutureTeak()
        {
            for (int i = 0; i <= LengthFuture; i++)
            {
                bool isChangetFuture = false;
                float t = 0;
                for (int x = 0; x < bodies.Count; x++)
                    if (bodies[x].FutureLength == i)
                        lock (bodies[x])
                        {
                            isChangetFuture = true;
                            for (int y = 0; y < bodies.Count; y++)
                                if (y > x || (y < x && bodies[y].FutureLength != i))
                                {
                                    float s = 0;
                                    s = bodies[x].SpeedCorrection(bodies[y], historySlow[i]);
                                    if (s > t) t = s;
                                }
                        }
                foreach (var x in bodies)
                    lock (x)
                        if (x.FutureLength == i)
                            x.MoveFuture(historySlow[i]);
                if (isChangetFuture)
                {
                    if (historySlow.Count <= i + 1)
                        historySlow.Add(1);

                    if (t * historySlow[i] > 0.1) historySlow[i + 1] = historySlow[i] / 2;
                    else if (t * historySlow[i] < 0.05) historySlow[i + 1] = historySlow[i] * 2;
                    if (historySlow[i] >= 1) historySlow[i + 1] = 1;
                }
            }
        }
    }
}
