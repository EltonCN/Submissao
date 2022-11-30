using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseExitDoor : MonoBehaviour {
    [SerializeField] GameObject player;
    [SerializeField] GameObject exitDoor;
    private bool doorClosed = false;

    void OnCollisionEnter( Collision collider ) {
        if ( !doorClosed && GameObject.ReferenceEquals(collider.gameObject, player) ) {
            exitDoor.GetComponent<MeshCollider>().enabled = true;
            doorClosed = true;
        }
    }
}