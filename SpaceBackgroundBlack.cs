using System.Drawing;

namespace орбитальная_механика
{
    public class SpaceBackgroundBlack : IBackground
    {
        public Bitmap GetBackground(Point offset, int Width, int Height)
        {
            return new Bitmap(Width, Height);
        }
    }
}
