using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class LoopTransition : MonoBehaviour
{
    private AsyncOperation scene;
    private static int currentLoop = -1;
    public static List<LoopScriptableObject> loopList;
    [SerializeField] LoopScriptableObject[] loop;
    [SerializeField] private GameObject player;

    void Start()
    {
        if (loopList == null)
        {
            loopList = new List<LoopScriptableObject>();
        }
    }

    public AsyncOperation PreLoadLoop(LoopScriptableObject loopToLoad)
    {
        scene = SceneManager.LoadSceneAsync(loopToLoad.sceneName, LoadSceneMode.Additive);

        scene.allowSceneActivation = false;

        //outras configurações de loop

        return scene;
    }

    public void UnloadLoop(LoopScriptableObject loopToUnload)
    {
        SceneManager.UnloadSceneAsync(loopToUnload.sceneName);
    }

    public void ActivateScene(AsyncOperation scene)
    {
        scene.allowSceneActivation = true;
    }

    public void LoadNextLoop()
    {
        currentLoop += 1;
        scene = PreLoadLoop(loopList[currentLoop]);
        ActivateScene(scene);
    }

    public void AddLoopToList(LoopScriptableObject newLoop)
    {
        loopList.Add(newLoop);
    }
}