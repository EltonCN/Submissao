using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaiseOnCollision : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    [SerializeField] string tag;
    [SerializeField] bool destroyOnRaise = false;
    [SerializeField] UnityEvent onCollision;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == tag || other.attachedRigidbody.gameObject.tag == tag)
        {
            onCollision.Invoke();
            gameEvent.Raise();

            if(destroyOnRaise)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
