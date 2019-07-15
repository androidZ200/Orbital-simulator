using System.Drawing;
using System.Windows.Forms;

namespace орбитальная_механика
{
    public interface IBackground
    {
        Bitmap GetBackground(Point offset, int Width, int Height);
    }

    public interface IControl
    {
        void KeyDown(Keys key);
        void KeyUp(Keys key);
    }
}
