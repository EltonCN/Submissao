using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseExitDoor : MonoBehaviour {
    [SerializeField] GameObject player;
    private GameObject exitDoor;
    private bool doorClosed = false;

    void Start() {
        exitDoor = GameObject.FindGameObjectWithTag( "PortaSaida" );
    }


    void OnCollisionEnter( Collision collider ) {
        if ( !doorClosed && collider.gameObject == player ) {
            exitDoor.GetComponent<MeshCollider>().enabled = true;
            doorClosed = true;
        }
    }

}
