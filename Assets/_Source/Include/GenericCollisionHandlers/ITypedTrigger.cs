using System;

namespace Include.GenericTriggers
{
    public interface ITypedTrigger<T>
    {
        event Action<T> TriggerEnter;
        event Action<T> TriggerExit;
    }
}