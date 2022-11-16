using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Sounds : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource UI;
    public AudioClip UI_clip;

    public void OnPointerEnter(PointerEventData ped)
    {
        UI.clip = UI_clip;
        UI.pitch = Random.Range(0.99f, 1.01f);
        UI.Play();
    }
}