using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region params

    [SerializeField] private MenuManager MenuManager;
    
    public static GameManager Instance;
    public GridSceneGameState state;

    public static event Action<GridSceneGameState> OnGameStateChanged;

    private int _round = 0;
    private GridSceneGameState _turn;

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GridSceneGameState.SelectColor);
    }

    public void UpdateGameState(GridSceneGameState newState)
    {
        state = newState;

        if (_round > 5) return;

        switch (state)
        {
            case GridSceneGameState.SelectColor:
                HandleSelectColor();
                break;
                
            case GridSceneGameState.WhitePlayerTurn:
                HandleWhitePlayerTurn();
                break;
            
            case GridSceneGameState.BlackPlayerTurn:
                HandleBackPlayerTurn();
                break;
            
            case GridSceneGameState.CalculatePieces:
                HandleCalculatePieces();
                break;
            
            case GridSceneGameState.Victory:
                break;
            
            case GridSceneGameState.Lose:
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        OnGameStateChanged?.Invoke(newState);

    }

    private void HandleCalculatePieces()
    {
        if (_round <= 5) return;
        UpdateGameState(GridSceneGameState.Victory);
        Debug.Log("There is no pieces left");
    }

    private async void HandleBackPlayerTurn()
    {
        Debug.Log("<color=black>Black</color> Player turn!");
        await Task.Delay(2000);

        _round++;

    }

    private async void HandleWhitePlayerTurn()
    {
        Debug.Log("<color=white>White</color> Player turn!");
        await Task.Delay(2000);

        _round++;

    }

    private void HandleSelectColor()
    {
        _turn = GridSceneGameState.WhitePlayerTurn;

    }

    public void ChangeTurn()
    {
        _turn = SwitchTurn();
        
        UpdateGameState(_turn);
    }

    private GridSceneGameState SwitchTurn()
    {
        return _turn == GridSceneGameState.BlackPlayerTurn
            ? GridSceneGameState.WhitePlayerTurn
            : GridSceneGameState.BlackPlayerTurn;
    }
    
}

public enum GridSceneGameState
{
    SelectColor,
    WhitePlayerTurn,
    BlackPlayerTurn,
    CalculatePieces,
    Victory,
    Lose
}
