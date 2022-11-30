using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseOnCollision : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    [SerializeField] string tag;
    [SerializeField] bool destroyOnRaise = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == tag || other.attachedRigidbody.gameObject.tag == tag)
        {
            gameEvent.Raise();

            if(destroyOnRaise)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
