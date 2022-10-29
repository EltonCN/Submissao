using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopLoader : MonoBehaviour
{
    AsyncOperation scene;

    public void PreLoadLoop(string sceneName)
    {
        scene = SceneManager.LoadSceneAsync(sceneName);

        //Para carregar loops aditivamente se for o caso
        //scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        scene.allowSceneActivation = false;
    }

    public void UnloadLoop(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void ActivateScene()
    {
        scene.allowSceneActivation = true;
    }
}
