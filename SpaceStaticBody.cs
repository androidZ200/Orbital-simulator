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

        public override void MoveFuture(float slow)
        {
            lastSpeed = PointF.Empty;
            newSpeed = PointF.Empty;
            base.MoveFuture(slow);
        }
        public override void GetPicture(Graphics g, Point center)
        {
            g.FillEllipse(new SolidBrush(color), center.X - 3, center.Y - 3, 7, 7);
        }
    }
}
