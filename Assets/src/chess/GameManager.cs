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
    private GameState _turn;

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

        if (_round > 5) return;

        switch (state)
        {
            case GameState.SelectColor:
                HandleSelectColor();
                break;
                
            case GameState.WhitePlayerTurn:
                HandleWhitePlayerTurn();
                break;
            
            case GameState.BlackPlayerTurn:
                HandleBackPlayerTurn();
                break;
            
            case GameState.CalculatePieces:
                HandleCalculatePieces();
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
        if (_round <= 5) return;
        UpdateGameState(GameState.Victory);
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
        _turn = GameState.WhitePlayerTurn;

    }

    public void ChangeTurn()
    {
        _turn = SwitchTurn();
        
        UpdateGameState(_turn);
    }

    private GameState SwitchTurn()
    {
        return _turn == GameState.BlackPlayerTurn
            ? GameState.WhitePlayerTurn
            : GameState.BlackPlayerTurn;
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
