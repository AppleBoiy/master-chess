using System;
using System.Collections.Generic;
using UnityEngine;
using static GameState;

public class GameManager : MonoBehaviour
{
    #region params
    
    public static GameManager Instance;
    
    public GameState State;
    
    private int _round = 0;
    private List<Piece> _blackPieces, _whitePieces;
    private bool _isEnd;
    
    #endregion

    private void UpdatePiecesLeft()
    {
        _blackPieces = PieceManager.Instance.CalBlackPiecesLeft();
        _whitePieces = PieceManager.Instance.CalWhitePiecesLeft();
        
        Debug.Log(
            $"<color=black>BLACK</color> has {_blackPieces.Count} left\n<color=white>WHITE</color> has {_whitePieces.Count} left");

        Debug.Log(State);
        
        _isEnd =  State != StartGame && (_whitePieces.Count == 0 || _blackPieces.Count == 0);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    { 
        UpdateGameState(StartGame);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (State)
            {
                case StartGame:
                    TileManager.Instance.GenerateTile();
                    PieceManager.Instance.SpawnWhitePieces();
                    PieceManager.Instance.SpawnBlackPieces();
                    break;
                
                case BlackTurn:
                    HandleBlackTurn();
                    break;
                    
                case WhiteTurn:
                    HandleWhiteTurn();
                    break;
                    
                case Win:
                    HandleWin();
                    break;
                    
                case Lose:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        
    }

    private void HandleWin()
    {
        Debug.Log("Game END..");
    }

    private void HandleBlackTurn()
    {
        Debug.Log("<color=black>BLACK</color> Player turn!");
        State = BlackTurn;
        _round++;
    }

    private void HandleWhiteTurn()
    {
        Debug.Log("<color=white>WHITE</color> Player turn!");
        State = WhiteTurn;
        _round++;

    }

    public void ChangeTurn()
    {

        Debug.Log($"<color=red>Current</color> player {State}");
        
        State = (State == BlackTurn) 
            ? WhiteTurn 
            : BlackTurn;
        
        Debug.Log($"<color=red>Update</color> to player {State}");
        
        UpdateGameState(State);
    }


}
