using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace орбитальная_механика
{
    public class SpaceBackgroundRadial : IBackground
    {
        private List<SpaceBody> bodies;

        public SpaceBackgroundRadial(List<SpaceBody> bodies)
        {
            this.bodies = bodies;
        }

        public Bitmap GetBackground(Point offset, int Width, int Height)
        {
            Bitmap bmp = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(bmp);
            for (int j = 0; j < 70; j++)
                for (int i = 0; i < 120; i++)
                    DrawLine(i * Width / 120, j * Height / 70, offset, g);
            return bmp;
        }

        private void DrawLine(int x, int y, Point offset, Graphics g)
        {
            Pen p = null;
            PointF t = new PointF(x, y);
            PointF prev = t;
            for (int k = 1; k < 5; k++)
            {
                PointF s = PointF.Empty;
                lock (bodies)
                    foreach (var xx in bodies)
                    {
                        float tx = t.X - xx.X - offset.X;
                        float ty = t.Y - xx.Y - offset.Y;
                        float R2 = tx * tx + ty * ty;
                        if (R2 < 36) return;
                        s.X += xx.Weight * ty / R2;
                        s.Y -= xx.Weight * tx / R2;
                    }
                float n = (float)Math.Sqrt(s.X * s.X + s.Y * s.Y);
                if (p == null) p = new Pen(Color.FromArgb((int)(255 * sigm(Math.Sqrt(s.X * s.X + s.Y * s.Y))), 114, 114, 114));
                t.X += s.X / n * 4;
                t.Y += s.Y / n * 4;
                lock (g)
                    g.DrawLine(p, prev, t);
                prev = t;
            }
        }

        private double sigm(double x)
        {
            return 2 / (1 + Math.Exp(-x * 2)) - 1;
        }
    }
}
