using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float horizontalVelocity;
    [SerializeField] float jumpForce = 1f;
    [SerializeField] Transform forwardReference;
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] bool allowJump = false;

    bool jumping = false;
    bool cancelada = false;


    Vector2 input;
    

    
    // Start is called before the first frame update
    void Start()
    {
        input = Vector2.zero;
    }

    public void ReceiveMovementInput(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        input *= horizontalVelocity;

        
    }

    void FixedUpdate()
    {
        Vector3 velocity = ( forwardReference.right * input.x + forwardReference.forward * input.y);

        velocity.y = rigidBody.velocity.y;

        if(velocity.y > 0 && !allowJump)
        {
            velocity.y = 0;
        }

        if(jumping && allowJump)
        {
            rigidBody.AddForce(new Vector3(0,jumpForce,0), ForceMode.Impulse);
            jumping = false;
        }

        rigidBody.velocity = velocity;
    }

    public void Jump(InputAction.CallbackContext context)
    {        
        if(context.phase == InputActionPhase.Canceled)
        {
            cancelada = true;
        }

        if(context.phase == InputActionPhase.Performed && Mathf.Abs(rigidBody.velocity.y) <= 0.1f  && cancelada)
        {
            jumping = true;
            cancelada = false;
        }
        else
        {
            jumping = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}