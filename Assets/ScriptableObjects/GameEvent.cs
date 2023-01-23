using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Event")]

public  abstract class GameEvent<T> : ScriptableObject
{
    private readonly List<GameEventListener<T>> listeners = new List<GameEventListener<T>>();

    // Raise event through different methods signatures

    public void Raise(T item)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(item);
        }
    }

    // Manage Listeners

    public void RegisterListener(GameEventListener listener)
    {
        // Si el listener no esta en la lista, se lo agrega.
        if (!listeners.Contains(listener)) listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener)) listeners.Remove(listener);
    }
}
