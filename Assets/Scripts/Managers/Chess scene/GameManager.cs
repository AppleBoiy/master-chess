using System;
using System.Collections.Generic;
using UnityEngine;
using static GameState;

public class GameManager : MonoBehaviour
{
    #region params
    
    private static readonly PieceManager PieceManager = PieceManager.Instance;
    private static readonly TileManager TileManager = TileManager.Instance;
    private static readonly Action<object> LOG = Debug.Log;
    public static GameManager Instance;
    
    private List<Piece> _blackPieces, _whitePieces;
    public GameState State;
    private int _round = 0;
    private bool _isEnd;
 
    #endregion

    private void UpdatePiecesLeft()
    {
        _blackPieces = PieceManager.CalBlackPiecesLeft();
        _whitePieces = PieceManager.CalWhitePiecesLeft();
        
        LOG(
            $"<color=black>BLACK</color> has {_blackPieces.Count} left\n<color=white>WHITE</color> has {_whitePieces.Count} left");

        LOG(State);
        
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
                    TileManager.GenerateTile();
                    PieceManager.SpawnWhitePieces();
                    PieceManager.SpawnBlackPieces();
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
        LOG("Game END..");
    }

    private void HandleBlackTurn()
    {
        LOG("<color=black>BLACK</color> Player turn!");
        State = BlackTurn;
        _round++;
    }

    private void HandleWhiteTurn()
    {
        LOG("<color=white>WHITE</color> Player turn!");
        State = WhiteTurn;
        _round++;

    }

    public void ChangeTurn()
    {

        LOG($"<color=red>Current</color> player {State}");
        
        State = (State == BlackTurn) 
            ? WhiteTurn 
            : BlackTurn;
        
        LOG($"<color=red>Update</color> to player {State}");
        
        UpdateGameState(State);
    }


}
