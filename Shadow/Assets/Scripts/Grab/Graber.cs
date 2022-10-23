using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Graber : MonoBehaviour
{
    [SerializeField] bool centerOnScreen;
    bool grabbing;
    Grabbable grabedObject;

    // Start is called before the first frame update
    void Start()
    {
        grabbing = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveCursorHit(RaycastHit hit, Vector2 position)
    {
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

        
        /*if(grabbing)
        {
            grabedObject.SetPosition(position);
        }*/
            
    }

    public void ReceiveCursorInput(InputAction.CallbackContext context)
    {
        if(grabbing)
        {
            Vector2 cursorPosition;
            if(centerOnScreen)
            {
                cursorPosition = new Vector2(Screen.width/2, Screen.height/2);
                
            }
            else
            {
                cursorPosition = context.ReadValue<Vector2>();
            }
            
            Ray ray = Camera.main.ScreenPointToRay(cursorPosition);

            grabedObject.SetPosition(ray.origin, ray.direction);
        }
    }
}
