using System.Collections;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.AudioListener;
using static UnityEngine.PlayerPrefs;
using static UnityEngine.SceneManagement.SceneManager;
using static UnityEngine.Screen;

public class MainMenuSceneManager : MonoBehaviour
{   
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeScale;
    [SerializeField] public Slider volumeSlider;
    
    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt;

    private bool _isFullScreen;
    public void StartGameSinglePlayer()
    {
        LoadScene("Chess Scene");
    }

    public void SetVolume(float volume)
    {   
        AudioListener.volume = volume;
        volumeScale.text = volume.ToString("0");
    }

    public void VolumeApply()
    {
        SetFloat("masterVolume", volume);
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
        SetInt("masterFullScreen", (_isFullScreen ? 1 : 0));
        fullScreen = _isFullScreen;
        StartCoroutine(ConfirmationBox());
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

