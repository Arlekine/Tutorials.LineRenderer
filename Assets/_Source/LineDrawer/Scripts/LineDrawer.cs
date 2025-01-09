using UnityEngine;

namespace LineRendererTutorial.Drawer
{
    public interface ILineDrawer
    {
        Renderer StartDrawing(Vector2 point);
        void UpdateDrawing(Vector2 point);
        void FinishDrawing(Vector2 point);
        void ForceStopCurrent();
        void Clear();
    }
}
