using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseExitDoor : MonoBehaviour {
    [SerializeField] bool doorClosed = false;
    [SerializeField] GameObject exitDoor;
    [SerializeField] GameObject closedDoor;
    [SerializeField] GameObject openedDoor;
    [SerializeField] UnityEvent onDoorClose;

    [SerializeField] AudioSource doorClose;
    [SerializeField] AudioClip doorCloseClip;

    void OnCollisionEnter( Collision collider ) {
        if ( !doorClosed && collider.gameObject.tag == "Player") {
            closeDoor();
            doorClose.Play();
        }
    }

    void closeDoor()
    {
        exitDoor.GetComponent<MeshCollider>().enabled = true;
        closedDoor.SetActive(true);
        openedDoor.SetActive(false);
        doorClosed = true;
        onDoorClose.Invoke();
        
    }

    void openDoor()
    {
        exitDoor.GetComponent<MeshCollider>().enabled = false;
        closedDoor.SetActive(false);
        openedDoor.SetActive(true);
        doorClosed = false;
    }

    void OnValidate()
    {
        if(doorClosed)
        {
            closeDoor();
        }
        else
        {
            openDoor();
        }
    }
}