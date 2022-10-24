using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Graber : MonoBehaviour
{
    [SerializeField] bool centerOnScreen;
    [SerializeField] InputActionReference cursorAction;
    bool grabbing;
    bool grabEnabled;
    Grabbable grabedObject;

    void Start()
    {
        grabbing = false;
        grabEnabled = false;
    }

    void FixedUpdate()
    {
        Vector2 cursorPosition = cursorAction.action.ReadValue<Vector2>();

        processCursorPosition(cursorPosition);
    }

    public void EnableGrab()
    {
        grabEnabled = true;
    }

    public void DisableGrab()
    {
        grabEnabled = false;
        
        if(grabbing)
        {
            grabbing = false;
            grabedObject.EndGrab();
        }
        
    }

    public void ReceiveCursorHit(RaycastHit hit, Vector2 position)
    {
        if(!grabEnabled)
        {
            return;
        }
        if(!grabbing)
        {
            Grabbable grabbable = null;

            Grabbable hitGrabbable = hit.transform.gameObject.GetComponent<Grabbable>();
            if(hitGrabbable != null)
            {
                grabbable = hitGrabbable;
            }
            else
            {
                Grabbable colliderGrabbable = hit.collider.gameObject.GetComponent<Grabbable>();

                if(colliderGrabbable != null)
                {
                    grabbable = colliderGrabbable;
                }
                else
                {
                    return;
                }
            }

            grabbing = true;
            grabedObject = grabbable;

            grabedObject.StartGrab();
        }
            
    }

    void processCursorPosition(Vector2 cursorPosition)
    {
        if(grabbing)
        {
            if(centerOnScreen)
            {
                cursorPosition = new Vector2(Screen.width/2, Screen.height/2);
                
            }

            Ray ray = Camera.main.ScreenPointToRay(cursorPosition);

            grabedObject.SetPosition(ray.origin, ray.direction);
        }
    }
}
