using System.Collections.Generic;
using UnityEngine;

namespace LineRendererTutorial.HappyGlass
{
    public class EdgeColliderPhysicsCreator : PointsBasedPhysicsCreator
    {
        public EdgeColliderPhysicsCreator(float minDrawingDistance, float edgeWidth) : base(minDrawingDistance, edgeWidth)
        {}

        protected override void CreateCollider(List<Vector2> localPoints, Rigidbody2D rigidbody)
        {
            var edgeCollider = rigidbody.gameObject.AddComponent<EdgeCollider2D>();
            edgeCollider.edgeRadius = _edgeWidth;
            edgeCollider.SetPoints(localPoints);
        }
    }
}