using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMenuBtn : MonoBehaviour
{
        [SerializeField] private Button backToMenuBtn;

        private void Awake()
        {
                backToMenuBtn?.onClick.AddListener(LoadScene);
        }

        private static void LoadScene()
        {
                SceneManager.LoadScene("Main Scene");
        }
}