using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ButtonRaiser : MonoBehaviour
{
    [Tooltip("Action that will raise events.")]
    [SerializeField] InputActionReference action;


    [Tooltip("Event to raise when button starts to be pressed.")]
    [SerializeField] GameEvent onStartEvent;

    [Tooltip("Event to raise when button finishes being pressed.")]
    [SerializeField] GameEvent onEndEvent;

    void OnEnable()
    {
        action.action.started += this.ReceiveButton;
        action.action.canceled += this.ReceiveButton;
    }

    void OnDisable()
    {
        action.action.started -= this.ReceiveButton;
        action.action.canceled -= this.ReceiveButton;
    }


    public void ReceiveButton(InputAction.CallbackContext context)
    {
        print("Teste "+ context.phase.ToString()+ " "+ context.started.ToString()+" "+context.canceled.ToString());
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