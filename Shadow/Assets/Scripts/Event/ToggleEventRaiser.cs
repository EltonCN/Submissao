using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using UnityEngine;
using Unity.Barracuda;

public class ToggleEventRaiser : MonoBehaviour
{
    [Tooltip("Event to raise in first raise.")]
    [SerializeField] GameEvent firstEvent;

    [Tooltip("Event to raise in second raise.")]
    [SerializeField] GameEvent secondEvent;

    bool raiseFirst;

    void Start()
    {
        raiseFirst = true;
    }

    public void Raise()
    {
        if(raiseFirst)
        {
            firstEvent.Raise();
        }
        else
        {
            secondEvent.Raise();
        }

        raiseFirst = !raiseFirst;
    }
}