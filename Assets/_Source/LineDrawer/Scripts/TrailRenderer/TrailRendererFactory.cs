using UnityEngine;

namespace LineRendererTutorial.Drawer
{
    public class TrailRendererFactory : ITrailRenderFactory
    {
        private TrailRenderer _trailPrefab;
        private Transform _parent;

        public TrailRendererFactory(TrailRenderer trailPrefab, Transform parent)
        {
            _trailPrefab = trailPrefab;
            _parent = parent;
        }

        public TrailRenderer Create()
        {
            return Object.Instantiate(_trailPrefab, _parent);
        }

        public void Destroy(TrailRenderer trailRenderer)
        {
            Object.Destroy(trailRenderer.gameObject);
        }
    }
}