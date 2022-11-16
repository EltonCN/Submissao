using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FootstepsSound : MonoBehaviour
{
    private float startingTime;
    private float currentTime;

    public float footstepFrequency;

    public AudioSource locomotionSource;
    public AudioClip[] footstepClips;

    bool moving;

    void Start()
    {      
        moving = false;
        currentTime = startingTime;
    }

    public void PlayFootstep(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Canceled)
        {
            moving = false;
            return;
        }
        moving = true;
    }

    void Update()
    {
        if (moving == true)
        {
            currentTime += 1 * Time.deltaTime;
        }

        if(moving == false)
        {
            currentTime = 0;
        }

        if (currentTime > footstepFrequency)
        {
            locomotionSource.clip = footstepClips[Random.Range(0, 6)];
            locomotionSource.pitch = Random.Range(0.8f, 1.2f);
            locomotionSource.Play();

            currentTime = 0;
        }
    }
 }