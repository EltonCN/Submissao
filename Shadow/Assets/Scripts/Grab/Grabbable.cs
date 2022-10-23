using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] float cameraDistance = 1f;
    [SerializeField] bool faceCamera = true;

    bool previousKinematic;

    // Start is called before the first frame update
    void Start()
    {
        if(rigidbody != null)
        {
            previousKinematic = rigidbody.isKinematic;
        }
        
    }

    public void SetPosition(Vector3 position)
    {
        if(rigidbody != null)
        {
            rigidbody.MovePosition(position);
        }
        else
        {
            this.transform.position = position;
        }
        
        if(faceCamera)
        {
            if(rigidbody != null)
            {
                rigidbody.MoveRotation(Camera.main.transform.rotation);
            }
            else
            {
                transform.rotation = Camera.main.transform.rotation;
            }
        }
    }

    public void SetPosition(Vector3 cameraOrigin, Vector3 direction)
    {
        Vector3 position = cameraOrigin + (cameraDistance * direction);

        SetPosition(position);
    }

    public void StartGrab()
    {
        if(rigidbody != null)
        {
            previousKinematic = rigidbody.isKinematic;
            rigidbody.isKinematic = true;
        }
    }

    public void EndGrab()
    {
        if(rigidbody != null)
        {
            rigidbody.isKinematic = previousKinematic;
        }
    }

    void OnValidate()
    {
        if(GetComponent<Rigidbody>() != null)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
    }
}
