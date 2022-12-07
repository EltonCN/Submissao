using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShadowGameOver : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject player;

    void onCollisionEnter(Collider other)
    {
        if (other.CompareTag(player.tag))
        {
            gameManager.GameOver();
        }
    }
}
