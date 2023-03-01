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
                    break;

                case CheckBlack:
                    break;
                
                case CheckWhite:
                    break;

                case Promotion:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
    }

    private void HandleWhiteTurn()
    {
        State = WhiteTurn;
        
    }

    /// <summary>
    /// Change game state from black player turn to white player turn
    /// Or change from white player turn to black player turn
    /// </summary>
    public void ChangeTurn()
    {
        State = (State == BlackTurn)
            ? WhiteTurn 
            : BlackTurn;
        
        UpdateGameState(State);
    }


}
