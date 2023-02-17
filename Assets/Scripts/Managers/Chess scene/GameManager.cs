using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region params
    
    public static GameManager Instance;
    
    public GameState State;
    
    private int _round = 0;

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    { 
        UpdateGameState(GameState.StartGame);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        if (_round > 5) return;

        switch (State)
        {
            case GameState.StartGame:
                TileManager.Instance.GenerateTile();
                PieceManager.Instance.SpawnWhitePieces();
                PieceManager.Instance.SpawnBlackPieces();
                break;
            
            case GameState.BlackTurn:
                HandleBackTurn();
                break;
                
            case GameState.WhiteTurn:
                HandleWhiteTurn();
                break;
                
            case GameState.Win:
            case GameState.Lose:
            default:
                throw new ArgumentOutOfRangeException();
        }
        

    }

    private void HandleCalculatePieces()
    {
        if (_round <= 5) return;
        UpdateGameState(GameState.Win);
        Debug.Log("There is no pieces left");
    }

    private void HandleBackTurn()
    {
        Debug.Log("<color=black>BLACK</color> Player turn!");
        _round++;
    }

    private void HandleWhiteTurn()
    {
        Debug.Log("<color=white>WHITE</color> Player turn!");
        _round++;

    }

    public void ChangeTurn()
    {
        State = (State == GameState.BlackTurn) 
            ? GameState.WhiteTurn 
            : GameState.BlackTurn;
        
        UpdateGameState(State);
    }


}
