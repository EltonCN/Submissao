using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(menuName ="Shadow/Game Event")]
public class GameEvent : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<EventListener> eventListeners = 
        new List<EventListener>();

    

    public void Raise()
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised(this.name);
    }

    public void Raise<T>(T arg)
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised<T>(this.name, arg);
    }

    public void RegisterListener(EventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(EventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }

    void OnValidate()
    {
        //Debug.Log(typeof(T));
    }
}