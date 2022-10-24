using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShadowGameOver : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    void onCollisionEnter()
    {
        gameManager.GameOver();
    }
}
