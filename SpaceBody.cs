using System;
using System.Collections.Generic;
using System.Drawing;

namespace орбитальная_механика
{
    public class SpaceBody
    {
        public float Weight = 0;
        public List<PointF> Trail = new List<PointF>();
        public List<SpeedPoint> Future = new List<SpeedPoint>();
        public string Name;
        public Color color = Color.White;
        public PointF point
        {
            get => Future[0].point;
            set { Future[0].point = value; Changet(); }
        }
        public float X
        {
            get => Future[0].point.X;
            set { Future[0].point.X = value; Changet(); }
        }
        public float Y
        {
            get => Future[0].point.Y;
            set { Future[0].point.Y = value; Changet(); }
        }
        public PointF speed
        {
            get => Future[0].speed;
            set { Future[0].speed = value; Changet(); }
        }
        public float sX
        {
            get => Future[0].speed.X;
            set { Future[0].speed.X = value; Changet(); }
        }
        public float sY
        {
            get => Future[0].speed.Y;
            set { Future[0].speed.Y = value; Changet(); }
        }
        public PointF lastPoint
        {
            get => Future[Future.Count - 1].point;
            set => Future[Future.Count - 1].point = value;
        }
        public float lX
        {
            get => Future[Future.Count - 1].point.X;
            set => Future[Future.Count - 1].point.X = value;
        }
        public float lY
        {
            get => Future[Future.Count - 1].point.Y;
            set => Future[Future.Count - 1].point.Y = value;
        }
        public PointF lastSpeed
        {
            get => Future[Future.Count - 1].speed;
            set => Future[Future.Count - 1].speed = value;
        }
        public float lsX
        {
            get => Future[Future.Count - 1].speed.X;
            set => Future[Future.Count - 1].speed.X = value;
        }
        public float lsY
        {
            get => Future[Future.Count - 1].speed.Y;
            set => Future[Future.Count - 1].speed.Y = value;
        }
        public int FutureLength
        {
            get => Future.Count - 1;
        }
        public static int MaxLengthTrail
        {
            get
            {
                return maxLengthTrail;
            }
            set
            {
                lock (lockTrail)
                {
                    maxLengthTrail = value;
                    if (maxLengthTrail < 1) maxLengthTrail = 1;
                }
            }
        }
        public static int LengthFuture
        {
            get
            {
                return lenghtFuture - 1;
            }
            set
            {
                lock (lockTrail)
                {
                    lenghtFuture = value + 1;
                    if (lenghtFuture < 1) lenghtFuture = 1;
                }
            }
        }

        public class SpeedPoint
        {
            public PointF point;
            public PointF speed;

            public SpeedPoint()
            {
                point = new PointF(0, 0);
                speed = new PointF(0, 0);
            }
            public SpeedPoint(PointF point, PointF speed)
            {
                this.point = point;
                this.speed = speed;
            }
        }

        protected static int maxLengthTrail = 50;
        protected static int lenghtFuture = 1;
        protected PointF newSpeed;
        protected static object lockTrail = new object();

        public SpaceBody(float pointX, float pointY, float Weight, string Name)
        {
            Future.Add(new SpeedPoint(new PointF(pointX, pointY), PointF.Empty));
            this.Weight = Weight;
            this.Name = Name;
        }
        public SpaceBody(PointF point, float Weight, string Name) : this(point.X, point.Y, Weight, Name)
        {
        }
        public SpaceBody(SpaceBody other) : this(other.X, other.Y, other.Weight, other.Name)
        {
            Trail = new List<PointF>(other.Trail);
            Future = new List<SpeedPoint>(other.Future);
            newSpeed = other.newSpeed;
            color = other.color;
        }

        public virtual void Move()
        {
            Trail.Add(Future[0].point);
            Future.RemoveAt(0);
            while (Trail.Count > MaxLengthTrail)
                Trail.RemoveAt(0);
        }
        public virtual void MoveFuture(float slow)
        {
            if (newSpeed == null) newSpeed = lastSpeed;
            SpeedPoint t = new SpeedPoint(new PointF(lX + newSpeed.X * slow, lY + newSpeed.Y * slow), new PointF(newSpeed.X, newSpeed.Y));
            Future.Add(t);
        }
        public virtual void GetPicture(Graphics g, Point center)
        {
            g.DrawEllipse(new Pen(color, 1), center.X - 2, center.Y - 2, 4, 4);
        }
        public virtual float SpeedCorrection(SpaceBody other, float slow)
        {
            if (other == this) return 0;
            int minFuture = Math.Min(FutureLength, other.FutureLength);
            lock (lockTrail)
                if (Distance2(other, minFuture) > 100)
                    return Math.Max(SpeedCorrection(this, other, slow, minFuture), SpeedCorrection(other, this, slow, minFuture));
                else Collision(other, minFuture);
            return 0;
        }
        public void Changet()
        {
            Changet(0);
        }
        public void Changet(int index)
        {
            if (Future.Count > index)
                lock (lockTrail)
                {
                    Future.RemoveRange(index + 1, Future.Count - index - 1);
                    newSpeed = lastSpeed;
                }
        }
        public void ChangetLengthFuture()
        {
            if (Future.Count > lenghtFuture)
                lock (lockTrail)
                {
                    Future.RemoveRange(lenghtFuture, Future.Count - lenghtFuture);
                    newSpeed = lastSpeed;
                }
        }
        public void ChangetLengthTrail()
        {
            if (Trail.Count > MaxLengthTrail)
                lock (lockTrail)
                    Trail.RemoveRange(0, Trail.Count - MaxLengthTrail);
        }

        protected void Collision(SpaceBody other, int indexFuture)
        {
            PointF otherSpeed, mySpeed;
            if (indexFuture != other.FutureLength) otherSpeed = new PointF(other.Future[indexFuture].speed.X, other.Future[indexFuture].speed.Y);
            else otherSpeed = new PointF(other.newSpeed.X, other.newSpeed.Y);
            if (indexFuture != FutureLength) mySpeed = new PointF(Future[indexFuture].speed.X, Future[indexFuture].speed.Y);
            else mySpeed = new PointF(newSpeed.X, newSpeed.Y);
            double AngleA = Math.Atan2(other.Future[indexFuture].point.Y - Future[indexFuture].point.Y,
                other.Future[indexFuture].point.X - Future[indexFuture].point.X);

            double speed1Module = Math.Sqrt(mySpeed.X * mySpeed.X + mySpeed.Y * mySpeed.Y);
            double speed2Module = Math.Sqrt(otherSpeed.X * otherSpeed.X + otherSpeed.Y * otherSpeed.Y);
            double speed1Angle = Math.Atan2(mySpeed.Y, mySpeed.X) - AngleA;
            double speed2Angle = Math.Atan2(otherSpeed.Y, otherSpeed.X) - AngleA;

            mySpeed.X = (float)(speed1Module * Math.Cos(speed1Angle));
            mySpeed.Y = (float)(speed1Module * Math.Sin(speed1Angle));
            otherSpeed.X = (float)(speed2Module * Math.Cos(speed2Angle));
            otherSpeed.Y = (float)(speed2Module * Math.Sin(speed2Angle));

            bool zeroWeight = false;
            if (Weight == 0 && other.Weight == 0)
            {
                Weight = other.Weight = 1;
                zeroWeight = true;
            }
            if (Weight != 0 && other.Weight != 0)
            {
                float t = mySpeed.X * Weight / other.Weight;
                mySpeed.X = otherSpeed.X * other.Weight / Weight;
                otherSpeed.X = t;

                Changet(indexFuture);
                other.Changet(indexFuture);
            }
            else if (Weight == 0)
            {
                mySpeed.X = 2 * otherSpeed.X - mySpeed.X;
                Changet(indexFuture);
            }
            else
            {
                otherSpeed.X = 2 * mySpeed.X - otherSpeed.X;
                other.Changet(indexFuture);
            }
            if (zeroWeight) Weight = other.Weight = 0;

            speed1Module = Math.Sqrt(mySpeed.X * mySpeed.X + mySpeed.Y * mySpeed.Y);
            speed2Module = Math.Sqrt(otherSpeed.X * otherSpeed.X + otherSpeed.Y * otherSpeed.Y);
            speed1Angle = Math.Atan2(mySpeed.Y, mySpeed.X) + AngleA;
            speed2Angle = Math.Atan2(otherSpeed.Y, otherSpeed.X) + AngleA;

            mySpeed.X = (float)(speed1Module * Math.Cos(speed1Angle));
            mySpeed.Y = (float)(speed1Module * Math.Sin(speed1Angle));
            otherSpeed.X = (float)(speed2Module * Math.Cos(speed2Angle));
            otherSpeed.Y = (float)(speed2Module * Math.Sin(speed2Angle));

            if (indexFuture == other.FutureLength) other.newSpeed = otherSpeed;
            if (indexFuture == FutureLength) newSpeed = mySpeed;
        }
        protected double Distance2(SpaceBody other, int indexFuture)
        {
            return (Future[indexFuture].point.X - other.Future[indexFuture].point.X) *
                (Future[indexFuture].point.X - other.Future[indexFuture].point.X) +
                (Future[indexFuture].point.Y - other.Future[indexFuture].point.Y) *
                (Future[indexFuture].point.Y - other.Future[indexFuture].point.Y);
        }
        protected static float SpeedCorrection(SpaceBody Object, SpaceBody Body, float slow, int indexFuture)
        {
            if (Body.Weight == 0) return 0;
            double R2 = Object.Distance2(Body, indexFuture);
            double R3 = Math.Pow(R2, 3.0 / 2);

            if (indexFuture != Object.FutureLength) Object.Changet(indexFuture);
            Object.newSpeed.X += (float)(Body.Weight * (Body.Future[indexFuture].point.X - Object.Future[indexFuture].point.X) / R3) * slow;
            Object.newSpeed.Y += (float)(Body.Weight * (Body.Future[indexFuture].point.Y - Object.Future[indexFuture].point.Y) / R3) * slow;

            return (float)(Body.Weight / R2);
        }
    }
}
