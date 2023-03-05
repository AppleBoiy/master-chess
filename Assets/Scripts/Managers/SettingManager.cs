using UnityEngine;
using UnityEngine.UI;
using static GameState;

public class SettingManager : MonoBehaviour
{
    private static GameState _currentPlayer;

    [SerializeField] private Button openSettingBtn, closeSettingBtn;

    private void Start()
    {
        openSettingBtn?.onClick.AddListener(OpenSetting);
        closeSettingBtn?.onClick.AddListener(CloseSetting);
    }

    private static void OpenSetting()
    {
        _currentPlayer = GameManager.Instance.state;
        
        GameManager.Instance.UpdateGameState(Setting);
    }

    private static void CloseSetting()
    {
        GameManager.Instance.UpdateGameState(_currentPlayer);
    }
}