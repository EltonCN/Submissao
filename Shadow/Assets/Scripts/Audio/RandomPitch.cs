using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] float rangeMin;
    [SerializeField] float rangeMax;

    public void Randomize()
    {
        audioSource.pitch = Random.Range(rangeMin, rangeMax);
    }

}