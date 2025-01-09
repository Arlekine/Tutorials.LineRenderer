using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Include
{
    public interface ICoroutineHandler
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine copoutine);
    }
}
