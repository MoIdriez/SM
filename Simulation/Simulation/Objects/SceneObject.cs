using System.Drawing;

namespace Engine
{
    public interface SceneObject
    {
        bool IsDrawn { get; set; }
        Pen Pen { get; set; }
        void Draw(Graphics g, Pen pen = null);
        Pen GetDefaultPen();
    }
}
