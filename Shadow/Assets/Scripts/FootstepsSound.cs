using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FootstepsSound : MonoBehaviour
{
    public float footstepFrequency;
    public AudioSource locomotionSource;
    public AudioClip[] footstepClips;

    void Start()
    {
        StartCoroutine(Footstep());
    }

    IEnumerator Footstep()
    {

        if (Input.GetButton("Vertical"))

        {
            PlayFootstep();
        }

        else if (Input.GetButton("Horizontal"))

        {
            PlayFootstep();
        }

        yield return new WaitForSeconds(footstepFrequency);

        StartCoroutine(Footstep());

    }

    void PlayFootstep()
    {
        locomotionSource.clip = footstepClips[Random.Range(0, 5)];
        locomotionSource.pitch = Random.Range(0.8f, 1.2f);
        locomotionSource.Play();
    }

}