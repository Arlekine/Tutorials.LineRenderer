using System;
using UnityEngine;

namespace LineRendererTutorial.LineInput
{
    public interface ILineInput
    {
        event Action<Vector2> Started;
        event Action<Vector2> Updated;
        event Action<Vector2> Finished;
    }
}