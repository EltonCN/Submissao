using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class LoopTransition : MonoBehaviour
{
    public static List<LoopScriptableObject> loopList;
    [SerializeField] private LoopScriptableObject loop;
    [SerializeField] private GameObject player;
    private LoopLoader loopLoader;

    void Start()
    {
        if (loopList == null)
        {
            loopList = new List<LoopScriptableObject>();
        }

        loopList.Add(loop);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(player.tag))
        {
            loopLoader.PreLoadLoop(loop.sceneName);
        }
    }

    public void CallActivateScene()
    {
        loopLoader.ActivateScene();
    }
}


//Para carregar loops aditivamente, se for o caso
// [Serializable]
// public class LoopTransition : MonoBehaviour
// {
//     public static List<LoopScriptableObject> loopList;
//     [SerializeField] private LoopScriptableObject loop;
//     [SerializeField] private GameObject player;
//     private bool isLoaded;
//     private LoopLoader loopLoader;

//     void Start()
//     {
//         if (loopList == null)
//         {
//              loopList = new List<LoopScriptableObject>();
//         }

//         loopList.Add(loop);

//         Scene checkScene = SceneManager.GetSceneByName(loop.sceneName);
//         if (checkScene.IsValid())
//         {
//             isLoaded = true;
//         }
//     }

//     void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag(player.tag) && !isLoaded)
//         {
//             loopLoader.PreLoadLoop(loop.sceneName);

//             isLoaded = true;
//         }
//     }

//     void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag(player.tag) && isLoaded)
//         {
//             loopLoader.UnloadLoop(loop.sceneName);
//             isLoaded = false;
//         }
//     }

//     public void CallActivateScene()
//     {
//         loopLoader.ActivateScene();
//     }
// }