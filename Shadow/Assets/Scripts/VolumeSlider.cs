using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Text volumeTextValue;

    // Start is called before the first frame update
    void Start()
    {
        LoadPrefVolume();
    }

    public void SetVolume(float volumeToSet)
    {
        volumeTextValue.text = volumeToSet.ToString("P", CultureInfo.CurrentCulture);
        volumeSlider.value = volumeToSet;
        AudioListener.volume = volumeToSet;
    }

    public void ChangeVolume()
    {
        SetVolume(volumeSlider.value);
        SetPrefVolume(volumeSlider.value);
    }

    public void SetPrefVolume(float volumeValue)
    {
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
    }

    public void LoadPrefVolume()
    {
        SetVolume(PlayerPrefs.GetFloat("VolumeValue", 0.5f));
    }

}
