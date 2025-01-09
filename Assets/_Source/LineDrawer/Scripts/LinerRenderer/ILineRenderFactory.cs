using UnityEngine;

namespace LineRendererTutorial.Drawer
{
    public interface ILineRenderFactory
    {
        LineRenderer Create();
        void Destroy(LineRenderer trailRenderer);
    }
}