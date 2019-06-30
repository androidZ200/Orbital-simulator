using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace орбитальная_механика
{
    public class SpaceBackgroundRetro : ISpaceBackground
    {
        public Bitmap GetBackground(Point offset, int Width, int Height)
        {
            Bitmap bmp = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(bmp);


            for (int i = 20; i >= 5; i--)
            {
                DrawDeepLine(Width, Height, 1000, g, offset, i);
                DrawGrill(g, Color.FromArgb(0, (byte)(255.0 / (i + 1)), 0), Width, Height, offset, 1000, (float)(1.0 / (i + 1)));
            }

            return bmp;
        }

        private Point Balance(Point t, int w, int h)
        {
            Func<int, int, int> x = (r, y) =>
            {
                while (r < 0 || r >= y)
                    if (r < 0) r += y;
                    else r -= y;
                return r;
            };
            t.X = x(t.X, w);
            t.Y = x(t.Y, h);
            return t;
        }
        private Point Compression(Point t, float factor)
        {
            return new Point((int)(t.X * factor), (int)(t.Y * factor));
        }
        private void DrawGrill(Graphics g, Color c, int w, int h, Point offset, int l, float factor)
        {
            offset = Compression(Balance(offset, l, l), factor);
            l = (int)(l * factor);
            Point center = new Point(w / 2 + offset.X, h / 2 + offset.Y);
            Pen p = new Pen(c, 1);
            int t = center.X;
            while (t >= 0)
            {
                g.DrawLine(p, t, 0, t, h);
                t -= l;
            }
            t = center.X + l;
            while (t < w)
            {
                g.DrawLine(p, t, 0, t, h);
                t += l;
            }

            t = center.Y;
            while (t >= 0)
            {
                g.DrawLine(p, 0, t, w, t);
                t -= l;
            }
            t = center.Y + l;
            while (t < h)
            {
                g.DrawLine(p, 0, t, w, t);
                t += l;
            }
        }
        private void DrawDeepLine(int w, int h, int l, Graphics g, Point offset, int layer)
        {
            offset = Balance(offset, l, l);
            float factorUp = (float)(1.0 / (layer + 1));
            float factorDown = (float)(1.0 / (layer + 2));
            Point center = new Point(w / 2, h / 2);

            for (int i = -7; i < 8; i++)
                for (int j = -7; j < 8; j++)
                {
                    Point beg = new Point(center.X + (int)((offset.X + i * l) * factorDown), center.Y + (int)((offset.Y + j * l) * factorDown));
                    Point end = new Point(center.X + (int)((offset.X + i * l) * factorUp), center.Y + (int)((offset.Y + j * l) * factorUp));
                    if (end != beg)
                    {
                        LinearGradientBrush linGrBrush = new LinearGradientBrush(
                            beg, end,
                            Color.FromArgb(0, (byte)(255 * factorDown), 0), Color.FromArgb(0, (byte)(255 * factorUp), 0));
                        Pen pen = new Pen(linGrBrush, 1);

                        g.DrawLine(pen, beg, end);
                    }
                }
        }
    }
}
