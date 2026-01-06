using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventGeneric<T> : ScriptableObject
{
    List<GameEventListenerGeneric<T>> listneners = new List<GameEventListenerGeneric<T>>();

    public void TriggerEvent(T payload)
    {
        for (int i = listneners.Count - 1; i >= 0; i--)
        {
            listneners[i].OnEventTriggered(payload);
        }
    }

    public void AddListener(GameEventListenerGeneric<T> listener)
    {
        listneners.Add(listener);
    }

    public void RemoveListener(GameEventListenerGeneric<T> listener)
    {
        listneners.Remove(listener);
    }
}
