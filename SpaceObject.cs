using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace орбитальная_механика
{
    public class SpaceObject
    {
        public PointF point = new PointF(0, 0);
        public PointF speed = new PointF(0, 0);
        public string Name;

        public virtual void Move(float slow)
        {
            point.X += speed.X * slow;
            point.Y += speed.Y * slow;
        }
        public virtual Bitmap GetPicture()
        {
            return new Bitmap(13, 13);
        }
        public virtual float SpeedCorrection(SpaceBody other, float slow)
        {
            return SpeedCorrection(this, other, slow);
        }

        protected double Distance2(SpaceObject other)
        {
            return (point.X - other.point.X) * (point.X - other.point.X) +
                (point.Y - other.point.Y) * (point.Y - other.point.Y);
        }
        protected static float SpeedCorrection(SpaceObject Object, SpaceBody Body, float slow)
        {
            double R2 = Object.Distance2(Body);
            double R3 = Math.Pow(R2, 3.0 / 2);

            Object.speed.X += (float)(Body.Weight * (Body.point.X - Object.point.X) / R3) * slow;
            Object.speed.Y += (float)(Body.Weight * (Body.point.Y - Object.point.Y) / R3) * slow;

            return (float)(Body.Weight / R2);
        }
    }
}
