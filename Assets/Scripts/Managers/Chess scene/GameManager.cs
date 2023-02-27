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

    /// <summary>
    /// Update game to next state
    /// </summary>
    /// <param name="newState">Next game state</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void UpdateGameState(GameState newState)
    {
        Action generateTile = TileManager.Instance.GenerateTile;
        var pieceManager = PieceManager.Instance;
        
        State = newState;
        
        //Game state handler
        switch (State)
            {
                case StartGame:
                    generateTile();
                    pieceManager.SpawnWhitePieces();
                    pieceManager.SpawnBlackPieces();
                    break;
                
                case BlackTurn:
                    break;
                    
                case WhiteTurn:
                    break;

                case End:
                    Debug.Log("<color=red>GAME IS END</color>");
                    break;

                case CheckBlack:
                    break;
                
                case CheckWhite:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
    }

    private void HandleWhiteTurn()
    {
        Debug.Log("<color=white>WHITE</color> Player turn!");
        State = WhiteTurn;
        
    }

    /// <summary>
    /// Change game state from black player turn to white player turn
    /// Or change from white player turn to black player turn
    /// </summary>
    public void ChangeTurn()
    {

        Debug.Log($"<color=red>Current</color> player {State}");
        
        State = (State is BlackTurn) 
            ? WhiteTurn 
            : BlackTurn;
        
        Debug.Log($"<color=red>Update</color> to player {State}");
        
        UpdateGameState(State);
    }


}
