using System.Drawing;

namespace орбитальная_механика
{
    public interface ISpaceBackground
    {
        Bitmap GetBackground(Point offset, int Width, int Height);
    }
}
