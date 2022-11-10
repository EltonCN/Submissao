using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] string newPhraseName;
    [SerializeField] PhoneMenu phoneMenu;
    [SerializeField] GameObject player;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(player.tag))
        {
            phoneMenu.changeMessage(newPhraseName);
        }
    }
}
