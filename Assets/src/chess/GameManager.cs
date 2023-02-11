using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region params

    [SerializeField] private MenuManager MenuManager;
    
    public static GameManager Instance;
    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    private int _round = 0;

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.SelectColor);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (state)
        {
            case GameState.SelectColor:
                HandleSelectColor();
                break;
                
            case GameState.WhitePlayerTurn:
                HandleWhitePlayerTurn();
                UpdateGameState(GameState.CalculatePieces);
                break;
            
            case GameState.BlackPlayerTurn:
                HandleBackPlayerTurn();
                break;
            
            case GameState.CalculatePieces:
                HandleCalculatePieces();
                UpdateGameState(GameState.CalculatePieces);
                break;
            
            case GameState.Victory:
                break;
            
            case GameState.Lose:
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        OnGameStateChanged?.Invoke(newState);

    }

    private void HandleCalculatePieces()
    {
        if (_round >= 5)
            return;
            
        Debug.Log("There is no pieces left");
        UpdateGameState(GameState.WhitePlayerTurn);
    }

    private async void HandleBackPlayerTurn()
    {
        Debug.Log("Black Player turn!");
        await Task.Delay(2000);

        _round += 1;
        
        PiecesHandler.Instance.CurrentPlayer(GameState.BlackPlayerTurn);
        
        UpdateGameState(GameState.WhitePlayerTurn);
    }

    private async void HandleWhitePlayerTurn()
    {
        Debug.Log("White Player turn!");
        await Task.Delay(2000);

        _round += 1;
        
        PiecesHandler.Instance.CurrentPlayer(GameState.WhitePlayerTurn);
        UpdateGameState(GameState.BlackPlayerTurn);
    }

    private void HandleSelectColor()
    {
        UpdateGameState(GameState.WhitePlayerTurn);
    }
    
}

public enum GameState
{
    SelectColor,
    WhitePlayerTurn,
    BlackPlayerTurn,
    CalculatePieces,
    Victory,
    Lose
}
