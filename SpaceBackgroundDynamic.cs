using System;
using System.Collections.Generic;
using System.Drawing;

namespace орбитальная_механика
{
    public abstract class SpaceBackgroundDynamic : IBackground
    {
        protected List<SpaceBody> bodies;
        public SpaceBackgroundDynamic(List<SpaceBody> bodies)
        {
            this.bodies = bodies;
        }
        public abstract Bitmap GetBackground(Point offset, int Width, int Height);

        protected double sigm(double x)
        {
            return 2 / (1 + Math.Exp(-x)) - 1;
        }
        protected double GetHeight(double x, double y)
        {
            double h = 0;
            lock (bodies)
                foreach (var b in bodies)
                {
                    double tx = x - b.X;
                    double ty = y - b.Y;
                    double R = Math.Sqrt(tx * tx + ty * ty);
                    if (R == 0) return 10E100;
                    h += b.Weight / R;
                }
            return h;
        }
        protected PointF GetSpeed(float x, float y)
        {
            PointF s = PointF.Empty;
            lock (bodies)
                foreach (var xx in bodies)
                {
                    float tx = x - xx.X;
                    float ty = y - xx.Y;
                    float R2 = (float)Math.Pow(tx * tx + ty * ty, 1.5);
                    if (R2 < 216) return PointF.Empty;
                    s.X -= xx.Weight * tx / R2;
                    s.Y -= xx.Weight * ty / R2;
                }
            return s;
        }
    }

    public class SpaceBackgroundCentral : SpaceBackgroundDynamic
    {
        public SpaceBackgroundCentral(List<SpaceBody> bodies) : base(bodies) { }
        public override Bitmap GetBackground(Point offset, int Width, int Height)
        {
            Bitmap bmp = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(bmp);
            int s = Width / 160;
            for (int j = 0; j < 50; j++)
                for (int i = 0; i < 80; i++)
                    DrawLine(i * Width / 80 + (j % 2 == 0 ? s : 0), j * Height / 50, offset, g);
            return bmp;
        }
        private void DrawLine(int x, int y, Point offset, Graphics g)
        {
            PointF t = new PointF(x, y);
            PointF prev = t;
            float power = -1;
            double h = Math.Abs(GetHeight(t.X - offset.X, t.Y - offset.Y));
            Pen p = new Pen(Color.FromArgb((int)(sigm(h*2) * 100), 140, 140, 190));
            for (int k = 1; k < 10; k++)
            {
                PointF s = GetSpeed(t.X - offset.X, t.Y - offset.Y);
                float n = (float)Math.Sqrt(s.X * s.X + s.Y * s.Y);
                if (power == -1) power = (float)sigm(Math.Pow(n, 1.0/3)*5)*5;
                t.X += s.X / n * power * 2;
                t.Y += s.Y / n * power * 2;
                lock (g)
                    g.DrawLine(p, prev, t);
                prev = t;
            }
        }
    }
    public class SpaceBackgroundRadial : SpaceBackgroundDynamic
    {
        public SpaceBackgroundRadial(List<SpaceBody> bodies) : base(bodies) { }
        public override Bitmap GetBackground(Point offset, int Width, int Height)
        {
            Bitmap bmp = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(bmp);
            for (int j = 0; j < 60; j++)
                for (int i = 0; i < 100; i++)
                    DrawLine(i * Width / 100, j * Height / 60, offset, g);
            return bmp;
        }
        private void DrawLine(int x, int y, Point offset, Graphics g)
        {
            PointF t = new PointF(x, y);
            PointF prev = t;
            double h = Math.Abs(GetHeight(t.X - offset.X, t.Y - offset.Y));
            Pen p = new Pen(Color.FromArgb((int)(sigm(h*2) * 180), 130, 130, 90));
            for (int k = 1; k < 5; k++)
            {
                PointF s = GetSpeed(t.X - offset.X, t.Y - offset.Y);
                float n = (float)Math.Sqrt(s.X * s.X + s.Y * s.Y);
                t.X -= s.Y / n * 4;
                t.Y += s.X / n * 4;
                lock (g)
                    g.DrawLine(p, prev, t);
                prev = t;
            }
        }
    }
    public class SpaceBackgroundColor : SpaceBackgroundDynamic
    {
        public SpaceBackgroundColor(List<SpaceBody> bodies) : base(bodies) { }
        public override Bitmap GetBackground(Point offset, int Width, int Height)
        {
            Bitmap bmp = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(bmp);
            for (int j = 0; j < Height; j += 20)
                for (int i = 0; i < Width; i += 20)
                    DrawLine(i, j, offset, g);
            return bmp;
        }
        private void DrawLine(int x, int y, Point offset, Graphics g)
        {
            double h = GetHeight(x + 10 - offset.X, y + 10 - offset.Y);
            Color r;
            if (h > 0) r = Color.FromArgb((int)(sigm(h * 2) * 180), 200, 100, 100);
            else r = Color.FromArgb((int)(sigm(-h * 2) * 150), 50, 50, 150);
            lock (g)
                g.FillRectangle(new SolidBrush(r), x, y, 20, 20);
        }
    }
}
