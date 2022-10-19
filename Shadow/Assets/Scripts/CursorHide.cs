using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHide : MonoBehaviour
{
    [SerializeField] bool hideOnStart = true;
    // Start is called before the first frame update
    void Start()
    {
        if(hideOnStart)
        {
            Hide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hide()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Show()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}