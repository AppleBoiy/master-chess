
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
    public GridState State;

    public static event Action<GridState> OnGameStateChanged;

    private int _round = 0;
    private GridState _turn;
    
    #endregion

    #region events

    private void Awake()
    {
        Instance = this;
        round.text = "";
    }

    private void Start()
    {
        UpdateGameState(GridState.GenerateGrid);
    }

    #endregion
    
    public void UpdateGameState(GridState newState)
    {
        stateText.text = newState.ToString();
        State = newState;

        switch (newState)
        {
            case GridState.GenerateGrid:
                Debug.Log("Generate grid");
                GridManager.Instance.GenerateGrid();
                break;
            
            case GridState.SpawnWhitePieces:
                Debug.Log("Spawn White Pieces");
                PieceManager.Instance.SpawnWhitePieces();
                break;
            
            case GridState.SpawnBlackPieces:
                Debug.Log("Spawn Black Pieces");
                PieceManager.Instance.SpawnBlackPieces();
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

    
    private Task<GridState> CalculatePieces()
    {
        if (_round <= 5) return Task.FromResult(_turn);
        
        Debug.Log("There is no pieces left");
        return Task.FromResult(GridState.Exit);
    }

    #region state Handler

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

    #endregion
    
    #region change state
    public async void ChangeTurn()
    {
        _turn = await SwitchTurn();
        if (_turn == GridState.Exit)
            return;
        UpdateGameState(_turn);
    }

    private async Task<GridState> SwitchTurn()
    {
        Task<GridState> task = CalculatePieces();
        _turn = await task;
        if (_turn == GridState.Exit)
            return GridState.Exit;
        
        return _turn == GridState.BlackPlayerTurn
            ? GridState.WhitePlayerTurn
            : GridState.BlackPlayerTurn;
    }
    #endregion

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
