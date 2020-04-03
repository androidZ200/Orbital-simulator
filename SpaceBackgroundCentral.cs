using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace орбитальная_механика
{
    public class SpaceBackgroundCentral : IBackground
    {
        private List<SpaceBody> bodies;

        public SpaceBackgroundCentral(List<SpaceBody> bodies)
        {
            this.bodies = bodies;
        }

        public Bitmap GetBackground(Point offset, int Width, int Height)
        {
            Bitmap bmp = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(bmp);
            int s = Width / 100;
            for (int j = 0; j < 30; j++)
                for (int i = 0; i < 50; i++)
                    DrawLine(i * Width / 50 + (j % 2 == 0 ? s : 0), j * Height / 30, offset, g);
            return bmp;
        }

        private void DrawLine(int x, int y, Point offset, Graphics g)
        {
            Pen p = new Pen(Color.FromArgb(50, 194, 194, 194));
            PointF t = new PointF(x, y);
            PointF prev = t;
            float power = -1;
            for (int k = 1; k < 20; k++)
            {
                PointF s = PointF.Empty;
                lock (bodies)
                    foreach (var xx in bodies)
                    {
                        float tx = t.X - xx.X - offset.X;
                        float ty = t.Y - xx.Y - offset.Y;
                        float R2 = tx * tx + ty * ty;
                        if (R2 < 36) return;
                        s.X -= xx.Weight * tx / R2;
                        s.Y -= xx.Weight * ty / R2;
                    }
                if (power == -1) power = (float)Math.Sqrt(s.X * s.X + s.Y * s.Y);
                float n = (float)Math.Sqrt(s.X * s.X + s.Y * s.Y);
                t.X += s.X / n * power;
                t.Y += s.Y / n * power;
                lock (g)
                    g.DrawLine(p, prev, t);
                prev = t;
            }
        }
    }
}
