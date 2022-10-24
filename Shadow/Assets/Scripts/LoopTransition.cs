using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class LoopTransition : MonoBehaviour
{
    [SerializeField] private LoopScriptableObject loop;

    void OnTriggerEnter()
    {
        SceneManager.LoadScene(loop.sceneName);
    }
}
