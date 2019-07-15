using System;
using System.Drawing;
using System.Windows.Forms;

namespace орбитальная_механика
{
    public class SpaceShip : SpaceBody, IControl
    {
        private bool gas = false;
        private object Lock = new object();

        public bool Gas
        {
            get
            {
                return gas;
            }
            private set
            {
                gas = value;
                if (gas) DrawFlyShop();
                else DrawShip();
            }

        }
        public bool RotateRight { get; private set; } = false;
        public bool RotateLeft { get; private set; } = false;
        public float Angle = 0;
        public float MaxPower = 0.01f;
        public Bitmap shipImage;

        public SpaceShip(float pointX, float pointY, string Name) : base(pointX, pointY, 0, Name)
        {
            DrawShip();
        }
        public SpaceShip(PointF point, string Name) : this(point.X, point.Y, Name)
        {
        }
        public SpaceShip(SpaceBody other) : base(other)
        {
            Weight = 0;
            if (other is SpaceShip)
            {
                Gas = ((SpaceShip)other).Gas;
                RotateLeft = ((SpaceShip)other).RotateLeft;
                RotateRight = ((SpaceShip)other).RotateRight;
                Angle = ((SpaceShip)other).Angle;
                MaxPower = ((SpaceShip)other).MaxPower;
                shipImage = (Bitmap)((SpaceShip)other).shipImage.Clone();
            }
            else DrawShip();
        }

        public void DrawShip()
        {
            lock (Lock)
            {
                shipImage = new Bitmap(13, 13);
                Graphics g = Graphics.FromImage(shipImage);
                Point[] points = { new Point(3, 2), new Point(12, 6), new Point(3, 10), new Point(6, 6) };
                g.FillPolygon(new SolidBrush(color), points);
            }
        }
        public void DrawFlyShop()
        {
            lock(Lock)
            {
                DrawShip();
                Graphics g = Graphics.FromImage(shipImage);
                Point[] points = { new Point(4, 4), new Point(0, 6), new Point(4, 8), new Point(6, 6) };
                g.FillPolygon(new SolidBrush(Color.Yellow), points);
            }
        }
        public override Bitmap GetPicture()
        {
            Bitmap result = new Bitmap(13, 13);
            Graphics g = Graphics.FromImage(result);
            g.TranslateTransform(6, 6);
            g.RotateTransform((float)(Angle * 180 / Math.PI));
            g.TranslateTransform(-6, -6);
            lock (Lock)
                g.DrawImage(shipImage, new Point(0, 0));
            return result;
        }
        public override float SpeedCorrection(SpaceBody other, float slow)
        {
            return base.SpeedCorrection(other, slow);
        }
        public override void Move(float slow)
        {
            if (Gas)
            {
                speed.Y += (float)Math.Sin(Angle) * MaxPower * slow;
                speed.X += (float)Math.Cos(Angle) * MaxPower * slow;
            }
            if (RotateRight) Angle += 0.05f * slow;
            if (RotateLeft) Angle -= 0.05f * slow;
            base.Move(slow);
        }
        public void KeyDown(Keys key)
        {
            if (key == Keys.W) Gas = true;
            if (key == Keys.A) RotateLeft = true;
            if (key == Keys.D) RotateRight = true;
        }
        public void KeyUp(Keys key)
        {
            if (key == Keys.W) Gas = false;
            if (key == Keys.A) RotateLeft = false;
            if (key == Keys.D) RotateRight = false;
        }
    }
}
