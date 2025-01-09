using UnityEngine;

namespace LineRendererTutorial.HappyGlass
{
    public interface ILinePhysicsCreator
    {
        void StartEditing(GameObject targetObject, Vector2 point);
        void UpdateEditing(Vector2 point);
        void FinishEditing(Vector2 point);
        void ForceStopCurrent();
    }
}