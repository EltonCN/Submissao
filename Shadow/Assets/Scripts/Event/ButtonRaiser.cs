using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ButtonRaiser : MonoBehaviour
{
    [Tooltip("Event to raise when button starts to be pressed.")]
    [SerializeField] GameEvent onStartEvent;

    [Tooltip("Event to raise when button finishes being pressed.")]
    [SerializeField] GameEvent onEndEvent;


    public void ReceiveButton(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            onStartEvent.Raise();
        }
        if(context.canceled)
        {
            onEndEvent.Raise();
        }
    }
}