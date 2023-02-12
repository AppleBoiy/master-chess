
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GridSceneManager : MonoBehaviour
{
    
    #region params

    [SerializeField] private TMP_Text stateText;
    [SerializeField] private TMP_Text round;
    
    public static GridSceneManager Instance;
    public GridState state;

    public static event Action<GridState> OnGameStateChanged;

    private int _round = 0;
    private GridState _turn;

    #endregion

    private void Awake()
    {
        Instance = this;
        round.text = "";
    }

    private void Start()
    {
        UpdateGameState(GridState.GenerateGrid);
    }

    private void UpdateGameState(GridState newState)
    {

        stateText.text = newState.ToString();
        
        switch (newState)
        {
            case GridState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GridState.SpawnWhitePieces:
                break;
            case GridState.SpawnBlackPieces:
                break;
            case GridState.WhitePlayerTurn:
                HandleWhitePlayerTurn();
                break;
            
            case GridState.BlackPlayerTurn:
                HandleBackPlayerTurn();
                break;
            
            case GridState.WhitePlayerWin:
                break;
            case GridState.BlackPlayerWin:
                break;
            case GridState.Exit:
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        
        OnGameStateChanged?.Invoke(newState);

    }

    private bool CalculatePieces()
    {
        if (_round <= 5) return true;
        UpdateGameState(GridState.Exit);
        Debug.Log("There is no pieces left");
        return false;
    }

    private async void HandleBackPlayerTurn()
    {
        Debug.Log("<color=black>Black</color> Player turn!");
        await Task.Delay(2000);
        
        round.text = $"Round {_round}";
        
        _round++;

    }

    private async void HandleWhitePlayerTurn()
    {
        Debug.Log("<color=white>White</color> Player turn!");
        await Task.Delay(2000);

        round.text = $"Round {_round}";
        
        _round++;

    }

    private void HandleSelectColor()
    {
        _turn = GridState.WhitePlayerTurn;

    }

    public void ChangeTurn()
    {
        if (!CalculatePieces())
        {
            return;
        }
        
        _turn = SwitchTurn();
        UpdateGameState(_turn);
    }

    private GridState SwitchTurn()
    {
        return _turn == GridState.BlackPlayerTurn
            ? GridState.WhitePlayerTurn
            : GridState.BlackPlayerTurn;
    }
    
}

public enum GridState
{
    GenerateGrid = 0,
    SpawnWhitePieces = 1,
    SpawnBlackPieces = 2,
    WhitePlayerTurn = 3,
    BlackPlayerTurn = 4,
    WhitePlayerWin = 5,
    BlackPlayerWin = 6,
    
    
    Exit,
}
