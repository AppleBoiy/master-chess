using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] Slider VolumeSlider;
    [SerializeField] AudioMixer masterMixer;

    public void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
    }

    public void SetVolume(float volume)
    {
        if(volume < 1)
        {
            volume = 1;
        }

        RefreshSlider(volume);
        PlayerPrefs.SetFloat("SavedMasterVolume", volume);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(volume / 100) * 20f);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(VolumeSlider.value);
    }

    public void RefreshSlider(float volume)
    {
        VolumeSlider.value = volume;
    }
}
