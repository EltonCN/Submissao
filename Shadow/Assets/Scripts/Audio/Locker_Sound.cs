using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker_Sound : MonoBehaviour
{
    [SerializeField] AudioSource lockerSource;
    [SerializeField] AudioClip[] lockerClip;

    [SerializeField] float lockerTimer;
    [SerializeField] float lockerTotal;

    [SerializeField] float delay;


    // Start is called before the first frame update
    void Start()
    {
        delay = Random.Range(15f, 80f);
        lockerSource.PlayDelayed(delay);

        StartCoroutine(LockerSound());        
    }

    IEnumerator LockerSound()
    {
        PlayLockerSound();

        
        lockerTimer = Random.Range(15f, 80f);
        lockerTotal = lockerSource.clip.length + lockerTimer;

        yield return new WaitForSeconds(lockerSource.clip.length + lockerTimer);

        StartCoroutine(LockerSound());

    }

    void PlayLockerSound()
    {
        lockerSource.clip = lockerClip[Random.Range(0, 12)];
        lockerSource.pitch = Random.Range(0.99f, 1.01f);
        lockerSource.PlayDelayed(delay);
    }
}
