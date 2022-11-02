using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Sounds : MonoBehaviour
{
    public AudioSource UI;
    public AudioClip UI_clip;

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Vertical") || Input.GetButtonDown("Horizontal"))
        {
            UI.clip = UI_clip;
            UI.pitch = Random.Range(0.99f, 1.02f);
            UI.Play();
        }
    }
}