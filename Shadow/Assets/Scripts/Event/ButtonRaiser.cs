using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ButtonRaiser : MonoBehaviour
{
    [SerializeField] GameEvent onStartEvent;
    [SerializeField] GameEvent onEndEvent;


    public void ReceiveButton(InputAction.CallbackContext context)
    {
        print(context.started.ToString()+" "+context.performed.ToString()+" "+context.canceled.ToString());
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