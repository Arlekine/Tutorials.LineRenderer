using UnityEngine;

namespace LineRendererTutorial.Drawer
{
    public interface ITrailRenderFactory
    {
        TrailRenderer Create();
        void Destroy(TrailRenderer trailRenderer);
    }
}