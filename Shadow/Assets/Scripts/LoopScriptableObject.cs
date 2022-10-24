using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Loop", menuName = "NewLoop")]
public class LoopScriptableObject : ScriptableObject
{
    [SerializeField] public string sceneName;
}
