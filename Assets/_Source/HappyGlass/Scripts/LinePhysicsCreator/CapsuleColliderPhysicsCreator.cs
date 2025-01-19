using System.Collections.Generic;
using UnityEngine;

namespace LineRendererTutorial.HappyGlass
{
    public class CapsuleColliderPhysicsCreator : PointsBasedPhysicsCreator
    {
        public CapsuleColliderPhysicsCreator(float minDrawingDistance, float edgeWidth) : base(minDrawingDistance, edgeWidth)
        {}

        protected override void CreateCollider(List<Vector2> localPoints, Rigidbody2D rigidbody)
        {
            if (localPoints.Count > 1)
            {
                for (int i = 1; i < localPoints.Count; i++)
                {
                    CreateCapsuleCollider(rigidbody.transform, localPoints[i-1], localPoints[i]);
                }
            }

            rigidbody.gameObject.AddComponent<CompositeCollider2D>().GenerateGeometry();
        }

        private void CreateCapsuleCollider(Transform parent, Vector2 pointA, Vector2 pointB)
        {
            var capsuleColliderGO = new GameObject("Capsule Collider");
            capsuleColliderGO.transform.SetParent(parent);

            var center = (pointB + pointA) * 0.5f;
            var direction = (pointB - pointA).normalized;
            var length = (pointB - pointA).magnitude;

            var capsule = capsuleColliderGO.AddComponent<CapsuleCollider2D>();
            
            capsule.transform.localPosition = center;
            capsuleColliderGO.transform.localRotation = Quaternion.LookRotation(Vector3.forward, direction);
            capsule.size = new Vector2(_edgeWidth, length);
        }
    }
}