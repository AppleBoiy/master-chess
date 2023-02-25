using System;
using System.Collections.Generic;
using UnityEngine;
using static GameState;

public class GameManager : MonoBehaviour
{
    #region params
    
    public static GameManager Instance;
    
    public GameState State;
    
    private List<Piece> _blackPieces, _whitePieces;
    private bool _isEnd;
    
    #endregion

    
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
        Action generateTile = TileManager.Instance.GenerateTile;
        var pieceManager = PieceManager.Instance;
        
        State = newState;
        
        
        switch (State)
            {
                case StartGame:
                    generateTile();
                    pieceManager.SpawnWhitePieces();
                    pieceManager.SpawnBlackPieces();
                    break;
                
                case BlackTurn:
                    HandleBlackTurn();
                    break;
                    
                case WhiteTurn:
                    HandleWhiteTurn();
                    break;

                case END:
                    Debug.Log("<color=red>GAME IS END</color>");
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        
    }

    private static void HandleWin()
    {
        Debug.Log("Game END..");
    }

    private void HandleBlackTurn()
    {
        Debug.Log("<color=black>BLACK</color> Player turn!");
        State = BlackTurn;
    }

    private void HandleWhiteTurn()
    {
        Debug.Log("<color=white>WHITE</color> Player turn!");
        State = WhiteTurn;
        
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
