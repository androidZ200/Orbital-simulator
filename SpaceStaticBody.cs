using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace орбитальная_механика
{
    class SpaceStaticBody : SpaceBody
    {
        public SpaceStaticBody(float pointX, float pointY, float Weight, string Name) : base(pointX, pointY, Weight, Name)
        {
        }
        public SpaceStaticBody(PointF point, float Weight, string Name) : base(point, Weight, Name)
        {
        }
        public SpaceStaticBody(SpaceBody other) : base(other)
        {
        }

        public override void Move(float slow)
        {
            speed = PointF.Empty;
        }
        public override Bitmap GetPicture()
        {
            Bitmap bmp = new Bitmap(13, 13);
            Graphics g = Graphics.FromImage(bmp);
            g.FillEllipse(new SolidBrush(color), 3, 3, 7, 7);
            return bmp;
        }
    }
}
