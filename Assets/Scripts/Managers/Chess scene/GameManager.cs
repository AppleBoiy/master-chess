using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using static GameState;


public class GameManager : MonoBehaviour
{
    #region params

    [Header("Win Scene")] 
    [SerializeField] private GameObject blackWinScene;
    [SerializeField] private GameObject whiteWinScene;

    [SerializeField] private GameObject mvpHolder;
    [SerializeField] private Image mvp;
    
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
    /// > This function updates the game state and calls the appropriate functions to generate the board
    /// and spawn the pieces
    /// </summary>
    /// <param name="newState">The new state to be updated to.</param>
    /// <returns>
    /// The return type is void.
    /// </returns>
    public void UpdateGameState(GameState newState)
    {
        state = newState;
        
        //Game state handler
        if (state is not StartGame) return;

        Action generateTile = TileManager.Instance.GenerateTile;
        var pieceManager = PieceManager.Instance;

        generateTile();
        pieceManager.SpawnWhitePieces();
        pieceManager.SpawnBlackPieces();
    }

    /// <summary>
    /// If the current state is BlackTurn, then change the state to WhiteTurn, otherwise change the
    /// state to BlackTurn
    /// </summary>
    public void ChangeTurn()
    {
        state = (state is BlackTurn)
            ? WhiteTurn 
            : BlackTurn;
        
        UpdateGameState(state);
    }

    public void BlackWin()
    {
        blackWinScene?.SetActive(true);
        Piece attacker = TileManager.Instance?.GetTile(WhiteTeam.KingPos).occupiedPiece;

        ShowMvp(attacker);
    }

    public void WhiteWin()
    {
        whiteWinScene?.SetActive(true);
        Piece attacker = TileManager.Instance?.GetTile(BlackTeam.KingPos).occupiedPiece;
     
        ShowMvp(attacker);

    }

    private void ShowMvp(Piece attacker)
    {
        mvpHolder.SetActive(true);
        
        mvp.sprite = attacker?.GetComponent<SpriteRenderer>().sprite;
        
    }
}
