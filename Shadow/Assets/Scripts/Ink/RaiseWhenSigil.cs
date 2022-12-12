using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseWhenSigil : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    [SerializeField] InkSigil sigil;

    public void ReceiveDetectedSigil( InkSigil receivedInk ) 
    {
        if ( receivedInk == sigil ) 
        {
            gameEvent.Raise();
        }
    }
}
