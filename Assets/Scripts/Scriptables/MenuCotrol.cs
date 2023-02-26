using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuCotrol : MonoBehaviour
{   
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeScale = null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("Confirmation")]
    [SerializeField] private GameObject comfirmationPrompt = null;

    private bool _isFullScreen;
    public void StartGameSinglePlayer()
    {
        SceneManager.LoadScene("Chess Scene");
    }

    public void setVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeScale.text = volume.ToString("0.0");
    }

    public void volumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public IEnumerator ConfirmationBox()
    {
        comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPrompt.SetActive(false);
    }

    public void setFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void fullScreenApply()
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

