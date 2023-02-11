using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class MenuManager : MonoBehaviour
{

    #region params

    [Header("Menu Manager")] 
    [SerializeField] private GameObject colorSelectPanel;
    
    [SerializeField] private Button testButton;
    [SerializeField] private TextMeshPro stateText;

    [SerializeField] private GameManager GameManager;
    #endregion

    void Start()
    {
    }
    
    // private void Awake()
    // {
    //     GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    // }
    //
    //
    // private void OnDestroy()
    // {
    //     GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    // }

    public void GameManagerOnOnGameStateChanged(GameState state)
    {
        colorSelectPanel.SetActive(state == GameState.SelectColor);
    }
    

    public async void ChangeTurn()
    {
        Debug.Log ("Change turn!!");
        
        testButton.interactable = false;

        await Task.Delay(2000);
        
        GameManager.ChangeTurn();

        testButton.interactable = true;
    }

    
}