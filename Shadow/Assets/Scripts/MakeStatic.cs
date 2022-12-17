using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeStatic : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] public bool isStatic = false;

    Vector3 position;
    Quaternion rotation;

    bool useGravity;
    bool isKinematic;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        captureTransform();

        if(rb)
        {
            useGravity = rb.useGravity;
            isKinematic = rb.isKinematic;
        }
        
    }

    void Update()
    {
        if(isStatic)
        {
            this.transform.position = position;
            this.transform.rotation = rotation;
        }
    }

    void captureTransform()
    {
        position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }

    public void SetStatic(bool isStatic)
    {
        if(!this.isStatic && isStatic)
        {
            if(rb)
            {
                useGravity = rb.useGravity;
                isKinematic = rb.isKinematic;
            }
        }
        if(this.isStatic && !isStatic)
        {
            if(rb)
            {
                rb.useGravity = useGravity;
                rb.isKinematic = isKinematic;
            }
        }

        this.isStatic = isStatic;
        captureTransform();
    }
}
