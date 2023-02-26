using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuSceneManager : MonoBehaviour
{   
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeScale;
    [SerializeField] private Slider volumeSlider;
    
    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt;

    private bool _isFullScreen;
    public void StartGameSinglePlayer()
    {
        SceneManager.LoadScene("Chess Scene");
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeScale.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    private IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void FullScreenApply()
    {
        PlayerPrefs.SetInt("masterFullScreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;
        StartCoroutine(ConfirmationBox());
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

