using System;
using System.Collections.Generic;

using UnityEngine;

using static GameState;


public class GameManager : MonoBehaviour
{
    #region params
    
    public static GameManager Instance;
    
    public GameState state;
    
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
    /// The UpdateGameState function is called when the game state changes. It takes in a new game state
    /// and updates the game state accordingly
    /// </summary>
    /// <param name="newState">This is the enum that contains all the possible states of the
    /// game.</param>
    public void UpdateGameState(GameState newState)
    {
        Action generateTile = TileManager.Instance.GenerateTile;
        var pieceManager = PieceManager.Instance;
        
        state = newState;
        
        
        //Game state handler
        switch (state)
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

    /// <summary>
    /// If the current state is BlackTurn, then change the state to WhiteTurn, otherwise change the
    /// state to BlackTurn
    /// </summary>
    public void ChangeTurn()
    {
        state = (state == BlackTurn)
            ? WhiteTurn 
            : BlackTurn;
        
        UpdateGameState(state);
    }


}
