using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{

    public float footstepFrequency = 0.5f;
    float footstepCounter;
    public AudioSource LocomotionSource;
    public AudioClip[] FootstepClips;

    public Vector2 pitchRange = new Vector2(0.8f, 1.2f);


    public void PlayFootstepSound(Vector3 velocity)
    {
        if(footstepCounter >= 1f / footstepFrequency)
        {
            footstepCounter = 0f;

            AudioFunctionalities.PlayRandomClip(LocomotionSource, FootstepClips, pitchRange.x, pitchRange.y);
        }

        footstepCounter += velocity.magnitude * Time.deltaTime;

    }

}
