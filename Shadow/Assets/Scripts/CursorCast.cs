using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CursorCast : MonoBehaviour
{
    [SerializeField] LayerMask layermask;
    [SerializeField] UnityEvent<RaycastHit> onHit;
    
    float maxDistance = 10;
    int layerMask;


    public void ReceiveCursorInput(InputAction.CallbackContext context)
    {
        Vector2 cursorPosition = context.ReadValue<Vector2>();        

        checkHit(cursorPosition);   
    }

    void checkHit(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;



        if(Physics.Raycast(ray, out hit, maxDistance, layermask))
        {
            onHit.Invoke(hit);
        }

    }
}
