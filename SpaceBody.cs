using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace орбитальная_механика
{
    public class SpaceBody
    {
        public PointF point = new PointF(0, 0);
        public PointF speed = new PointF(0, 0);
        public float Weight = 0;
        public Queue<PointF> Trail = new Queue<PointF>();
        public int MaxLengthTrail = 50;
        public string Name;
        public Color color = Color.White;

        public SpaceBody(float pointX, float pointY, float Weight, string Name)
        {
            point.X = pointX;
            point.Y = pointY;
            this.Weight = Weight;
            this.Name = Name;
        }
        public SpaceBody(PointF point, float Weight, string Name) : this(point.X, point.Y, Weight, Name)
        {
        }
        public SpaceBody(SpaceBody other)
        {
            point.X = other.point.X;
            point.Y = other.point.Y;
            speed.X = other.speed.X;
            speed.Y = other.speed.Y;
            Weight = other.Weight;
            Trail = new Queue<PointF>(other.Trail);
            MaxLengthTrail = other.MaxLengthTrail;
            Name = other.Name;
            color = other.color;
        }

        public virtual void Move(float slow)
        {
            point.X += speed.X * slow;
            point.Y += speed.Y * slow;
            Trail.Enqueue(point);
            while (Trail.Count > MaxLengthTrail)
                Trail.Dequeue();
        }
        public float SpeedCorrection(SpaceBody other, float slow)
        {
            double R2 = (point.X - other.point.X) * (point.X - other.point.X) + (point.Y - other.point.Y) * (point.Y - other.point.Y);
            double R3 = Math.Pow(R2, 3.0 / 2);


            if (R2 < 25)
                Collision(other);
            else
            {
                speed.X += (float)(other.Weight * (other.point.X - point.X) / R3) * slow;
                speed.Y += (float)(other.Weight * (other.point.Y - point.Y) / R3) * slow;

                other.speed.X += (float)(Weight * (point.X - other.point.X) / R3) * slow;
                other.speed.Y += (float)(Weight * (point.Y - other.point.Y) / R3) * slow;
            }

            return (float)(Math.Max(Weight, other.Weight) / R2);
        }
        public virtual Bitmap GetPicture()
        {
            Bitmap bmp = new Bitmap(13, 13);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawEllipse(new Pen(color, 1), 4, 4, 4, 4);
            return bmp;
        }

        private void Collision(SpaceBody other)
        {
            double AngleA = Math.Atan2(other.point.Y - point.Y, other.point.X - point.X);

            double speed1Module = Math.Sqrt(speed.X * speed.X + speed.Y * speed.Y);
            double speed2Module = Math.Sqrt(other.speed.X * other.speed.X + other.speed.Y * other.speed.Y);
            double speed1Angle = Math.Atan2(speed.Y, speed.X) - AngleA;
            double speed2Angle = Math.Atan2(other.speed.Y, other.speed.X) - AngleA;

            speed.X = (float)(speed1Module * Math.Cos(speed1Angle));
            speed.Y = (float)(speed1Module * Math.Sin(speed1Angle));
            other.speed.X = (float)(speed2Module * Math.Cos(speed2Angle));
            other.speed.Y = (float)(speed2Module * Math.Sin(speed2Angle));

            bool zeroWeight = false;
            if(Weight == 0 && other.Weight == 0)
            {
                Weight = other.Weight = 1;
                zeroWeight = true;
            }
            if (Weight != 0 && other.Weight != 0)
            {
                float t = speed.X * Weight / other.Weight;
                speed.X = other.speed.X * other.Weight / Weight;
                other.speed.X = t;
            }
            else
                if (Weight == 0) ColisionZero(this, other);
                else ColisionZero(other, this);
            if (zeroWeight) Weight = other.Weight = 0;

            speed1Module = Math.Sqrt(speed.X * speed.X + speed.Y * speed.Y);
            speed2Module = Math.Sqrt(other.speed.X * other.speed.X + other.speed.Y * other.speed.Y);
            speed1Angle = Math.Atan2(speed.Y, speed.X) + AngleA;
            speed2Angle = Math.Atan2(other.speed.Y, other.speed.X) + AngleA;

            speed.X = (float)(speed1Module * Math.Cos(speed1Angle));
            speed.Y = (float)(speed1Module * Math.Sin(speed1Angle));
            other.speed.X = (float)(speed2Module * Math.Cos(speed2Angle));
            other.speed.Y = (float)(speed2Module * Math.Sin(speed2Angle));
        }
        private static void ColisionZero(SpaceBody zero, SpaceBody nozero)
        {
            zero.speed.X *= -1;
            zero.speed.X += nozero.speed.X;
        }
    }
}
