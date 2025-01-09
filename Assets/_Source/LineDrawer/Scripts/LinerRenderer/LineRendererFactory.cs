using UnityEngine;

namespace LineRendererTutorial.Drawer
{
    public class LineRendererFactory : ILineRenderFactory
    {
        private LineRenderer _lineRendererPrefab;
        private Transform _parent;

        public LineRendererFactory(LineRenderer lineRendererPrefab, Transform parent)
        {
            _lineRendererPrefab = lineRendererPrefab;
            _parent = parent;
        }

        public LineRenderer Create()
        {
            return Object.Instantiate(_lineRendererPrefab, _parent);
        }

        public void Destroy(LineRenderer lineRenderer)
        {
            Object.Destroy(lineRenderer.gameObject);
        }
    }
}