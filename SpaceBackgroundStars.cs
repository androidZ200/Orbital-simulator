using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace орбитальная_механика
{
    public class SpaceBackgroundStars : ISpaceBackground
    {
        private struct Star
        {
            public Point point;
            public Color color;

            public Star(Point p, Color c)
            {
                point = p;
                color = c;
            }
        }

        private HashSet<Star> BackgroundDeep = new HashSet<Star>();
        private HashSet<Star> BackgroundMiddle1 = new HashSet<Star>();
        private HashSet<Star> BackgroundMiddle2 = new HashSet<Star>();
        private HashSet<Star> BackgroundMiddle3 = new HashSet<Star>();
        private HashSet<Star> BackgroundMiddle4 = new HashSet<Star>();
        private Size size = new Size(1280, 720);
        private Random rand = new Random();

        public SpaceBackgroundStars()
        {
            for (int i = 0; i < 200; i++)
                if (i < 100)
                {
                    DrawPoint(BackgroundDeep, Color.White);
                    DrawPoint(BackgroundMiddle1, Color.White);
                    DrawPoint(BackgroundMiddle2, Color.White);
                    DrawPoint(BackgroundMiddle3, Color.White);
                    DrawPoint(BackgroundMiddle4, Color.White);
                }
                else if (i < 133)
                {
                    DrawPoint(BackgroundMiddle1, Color.White);
                    DrawPoint(BackgroundMiddle2, Color.Yellow);
                    DrawPoint(BackgroundMiddle3, Color.Cyan);
                    DrawPoint(BackgroundMiddle4, Color.Red);
                }
                else if (i < 166)
                {
                    DrawPoint(BackgroundMiddle2, Color.Cyan);
                    DrawPoint(BackgroundMiddle3, Color.Red);
                    DrawPoint(BackgroundMiddle4, Color.Yellow);
                }
                else
                {
                    DrawPoint(BackgroundMiddle2, Color.Red);
                    DrawPoint(BackgroundMiddle3, Color.Yellow);
                    DrawPoint(BackgroundMiddle4, Color.Cyan);
                }
        }

        public Bitmap GetBackground(Point offset, int Width, int Height)
        {
            Bitmap t = new Bitmap(size.Width, size.Height);

            DrawImage(t, BackgroundDeep, Balance(Compression(offset, 0f)));
            DrawImage(t, BackgroundMiddle1, Balance(Compression(offset, 0.03f)));
            DrawImage(t, BackgroundMiddle2, Balance(Compression(offset, 0.06f)));
            DrawImage(t, BackgroundMiddle3, Balance(Compression(offset, 0.12f)));
            DrawImage(t, BackgroundMiddle4, Balance(Compression(offset, 0.15f)));

            return t;
        }

        private void DrawImage(Bitmap bmp, HashSet<Star> image, Point offset)
        {
            foreach(var x in image)
            {
                Point t = Balance(new Point(x.point.X + offset.X, x.point.Y + offset.Y));
                bmp.SetPixel(t.X, t.Y, x.color);
            }
        }
        private Point Balance(Point t)
        {
            Func<int, int, int> x = (r, y) =>
            {
                while (r < 0 || r >= y)
                    if (r < 0) r += y;
                    else r -= y;
                return r;
            };
            t.X = x(t.X, size.Width);
            t.Y = x(t.Y, size.Height);
            return t;
        }
        private Point Compression(Point t, float factor)
        {
            return new Point((int)(t.X * factor), (int)(t.Y * factor));
        }
        private void DrawPoint(HashSet<Star> b, Color c)
        {
            b.Add(new Star(new Point(rand.Next(size.Width), rand.Next(size.Height)), c));
        }
    }
}
