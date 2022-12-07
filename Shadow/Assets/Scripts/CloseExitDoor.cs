using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseExitDoor : MonoBehaviour {
    [SerializeField] GameObject player;
    [SerializeField] GameObject exitDoor;
    [SerializeField] UnityEvent onDoorClose;
    private bool doorClosed = false;

    void OnCollisionEnter( Collision collider ) {
        if ( !doorClosed && GameObject.ReferenceEquals(collider.gameObject, player) ) {
            exitDoor.GetComponent<MeshCollider>().enabled = true;
            doorClosed = true;
            onDoorClose.Invoke();
        }
    }
}