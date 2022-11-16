using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Behavior : MonoBehaviour
{
    public AudioSource bgm;
    public AudioClip loop1;

    private float timer;
    private float total;
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Bgm_b());
    }

    IEnumerator Bgm_b()
    {
        PlayBGM();

        timer = Random.Range(30f, 90f);
        total = bgm.clip.length + timer;
        
        yield return new WaitForSeconds(bgm.clip.length + timer);

        StartCoroutine(Bgm_b());
    }

    void PlayBGM()
    {
        bgm.clip = loop1;
        bgm.Play();
    }
}