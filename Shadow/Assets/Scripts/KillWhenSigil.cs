using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillWhenSigil : MonoBehaviour {
    [SerializeField] InkSigil killSigil;

    public void ReceiveDetectedSigil( InkSigil receivedInk ) {
        if ( receivedInk == killSigil ) {
            Destroy( this.gameObject );
        }
    }
}
