using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

public class Start : MonoBehaviour {
    public void GoToScene(string sceneName)
    {
        LoadScene(sceneName);
    }

    public void QuitApp() {
        Application.Quit();
        Debug.Log("Already Quit");
    }
}
