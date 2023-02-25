using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour {
    public void goToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void quitApp() {
        Application.Quit();
        Debug.Log("Already Quit");
    }
}
