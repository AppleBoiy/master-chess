using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using static Faction;
using static GameState;
using static IPiecesInGame;
using static MenuManager;
using static Piece;
using static PieceManager;
using static Roll;


public sealed class Tile : MonoBehaviour
{
    #region params

    [Header("Tile Unique")]
    [SerializeField] private Sprite baseTile;
    [SerializeField] private Sprite offsetTile;
    [SerializeField] private GameObject highlight;

    [Space(3)] [Header("Piece info frame")] 
    [SerializeField] private GameObject whiteFrame;
    [SerializeField] private GameObject blackFrame;
    
    private Vector2 _pos;

    [CanBeNull]
    public Piece occupiedPiece;

    #endregion
    
    
    /// <summary>
    /// It sets the sprite of the tile to either the base tile or the offset tile, depending on the
    /// value of the isOffset parameter
    /// </summary>
    /// <param name="isOffset">This is a boolean that determines whether the tile is an offset tile or
    /// not.</param>
    /// <param name="pos">The position of the tile.</param>
    public void Init(bool isOffset, Vector2 pos)
    {
        Transform child = transform.GetChild(1);
        SpriteRenderer componentInChildren = child.GetComponentInChildren<SpriteRenderer>();
        
        componentInChildren.sprite = isOffset ? baseTile : offsetTile;
        
        _pos = pos;
    }

    #region Getter

    
    /// <summary>
    /// > If any of the tiles in the current piece's move are walkable, return true
    /// </summary>
    private bool Walkable() => CurrentPieceMove.Any(OnWalkableTile);

    /// <summary>
    /// If the position of the player is the same as the position of the tile, then return true
    /// </summary>
    /// <param name="pos">The position of the tile.</param>
    private bool OnWalkableTile(Vector2 pos) => _pos == pos;


    public Vector2 GetPos() => _pos;
    
    #endregion
    
    #region Mouse action

    
    /// <summary>
    /// When the mouse enters the tile, the tile's highlight is activated and the tile's information is
    /// displayed in the menu
    /// </summary>
    /// <returns>
    /// The return value is the value of the last expression evaluated in the function.
    /// </returns>
    private void OnMouseEnter()
    {
        //Promotion scene player can't interaction with board
        if (GameManager.Instance.state is Promotion or Setting or End) return;

        switch (occupiedPiece)
        {
            case { faction: Black }:
                blackFrame.SetActive(true);
                break;
            case { faction: White }:
                whiteFrame.SetActive(true);
                break;
        }
        
        highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }

    /// <summary>
    /// When the mouse exits the tile, the highlight is turned off, the tile info is hidden, and the
    /// black and white frames are turned off
    /// </summary>
    private void OnMouseExit()
    {
        highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
        
        blackFrame.SetActive(false);
        whiteFrame.SetActive(false);

    }

    /// <summary>
    /// The function checks if the tile is occupied by a piece and if the piece is a black piece. If it
    /// is, then the piece is selected and its legal moves are calculated. If the piece is not a black
    /// piece, then the piece is destroyed and the turn is changed
    /// </summary>
    /// <returns>
    /// a boolean value.
    /// </returns>
    private void OnMouseDown()
    {
        if (GameManager.Instance.state is Setting or Promotion) return;
            
        ResetMove();

        Action changeTurn = GameManager.Instance.ChangeTurn;
        var instanceState = GameManager.Instance.state;
        
        /* Checking the state of the game and the piece that is on the tile. */
        switch (instanceState, occupiedPiece)
        {
            /* Checking if the tile is occupied by a piece and if the piece is a black piece. If it is,
            then the piece is selected and its legal moves are calculated. If the piece is not a
            black piece, then the piece is destroyed and the turn is changed. */
            case (BlackTurn, not null) : if (BlackMoveOccupiedTile(changeTurn)) return; break;
            
            /* Checking if the tile is unreachable or if the piece is moved. If either of these
            conditions are true, then the function returns. */
            case (BlackTurn, _) when UnreachableTile(): return;
            case (BlackTurn, _) when MovePiece(Black): return;
            case (BlackTurn, _): changeTurn(); break;
            
            /* Checking if the tile is occupied by a piece and if the piece is a white piece. If it is,
            then the piece is selected and its legal moves are calculated. If the piece is not a
            white piece, then the piece is destroyed and the turn is changed. */
            case (WhiteTurn, not null): if (WhiteMoveOccupiedTile(changeTurn)) return; break;
            
            
            /* This is checking if the piece on the tile is a white piece. If it is, then the piece
            is selected and its legal moves are calculated. */
            case (WhiteTurn, _) when UnreachableTile(): return;
            case (WhiteTurn, _) when MovePiece(White): return;
            case (WhiteTurn, _): changeTurn(); break;
            
        }
        
        ReloadPiecesLeftInGame();
    }

    #endregion

    #region Move piece on tile

    /// <summary>
    /// If the tile is occupied by a white piece, then select it and show its legal moves. If the tile
    /// is occupied by a black piece, then attack it and move the white piece to the tile
    /// </summary>
    /// <param name="changeTurn">A delegate type that represents a method that takes no parameters and
    /// returns void.</param>
    /// <returns>
    /// The return type is a bool. The return value is true if the tile is unreachable.
    /// </returns>
    private bool WhiteMoveOccupiedTile(Action changeTurn)
    {
        switch (occupiedPiece)
        {
            /* This is checking if the piece on the tile is a white piece. If it is, then the piece
                    is selected and its legal moves are calculated. */
            case { faction: White }:
                SetSelectedPiece(occupiedPiece);
                ShowNormalMove(occupiedPiece);

                ReloadPiecesLeftInGame();
                break;

            //Attack phase
            case { faction: Black }:
            {
                if (UnreachableTile()) return true;

                //Attack (Destroy enemy game object) 
                var blackPiece = (BlackPieces)occupiedPiece;
                if (blackPiece.roll is King) GameManager.Instance.UpdateGameState(End);
                Destroy(blackPiece.gameObject);

                if (MovePiece(White)) return true;

                changeTurn();
                break;
            }
        }

        return false;
    }

    /// <summary>
    /// If the tile is occupied by a black piece, then select it and show its legal moves. If the tile
    /// is occupied by a white piece, then attack it
    /// </summary>
    /// <param name="changeTurn">A delegate type that represents a method that takes no parameters and
    /// returns void.</param>
    /// <returns>
    /// A boolean value.
    /// </returns>
    private bool BlackMoveOccupiedTile(Action changeTurn)
    {
        switch (occupiedPiece)
        {
            /* Checking if the piece on the tile is a black piece. If it is, then the piece
                                                is selected and its legal moves are calculated. */
            case { faction: Black }:
                SetSelectedPiece(occupiedPiece);
                ShowNormalMove(occupiedPiece);

                ReloadPiecesLeftInGame();
                break;
            /* Checking if the piece on the tile is a black piece. If it is, then the piece
                                is selected and its legal moves are calculated. */
            case { faction: White }:
            {
                if (UnreachableTile()) return true;
                //Attack (Destroy enemy game object)
                var whitePiece = (WhitePieces)occupiedPiece;
                if (whitePiece.roll is King) GameManager.Instance.UpdateGameState(End);
                Destroy(whitePiece.gameObject);
                if (MovePiece(Black)) return true;
                changeTurn();
                break;
            }
        }

        return false;
    }

    /// <summary>
    /// If the selected piece is null or the tile is not walkable, then the function returns true.
    /// Otherwise, if the selected piece is a pawn, then the isFirstMove variable is set to false
    /// </summary>
    /// <returns>
    /// a boolean value.
    /// </returns>
    private bool UnreachableTile()
    {
        /* This is checking if the selected piece is null or if the tile is not walkable.
                    If either of these
                    conditions are true, then the function returns. */
        if (SelectedPiece is null || !Walkable()) return true;

        /* This is checking if the selected piece is a pawn. If it is, then the isFirstMove
                    variable is set to false. */
        if (SelectedPiece.roll is Pawn) SelectedPiece.isFirstMove = false;
        return false;
    }

    /// <summary>
    /// This function is used to move the selected piece to the selected tile
    /// </summary>
    /// <param name="faction">The faction of the player who is currently playing.</param>
    /// <returns>
    /// a boolean value.
    /// </returns>
    private bool MovePiece(Faction faction)
    {
        SetPiece(SelectedPiece);
        if (GameManager.Instance.state is End) return true;

        //Check this pawn is ready to promotion or not
        if (SelectedPiece.CheckPawnPromotion()) GameManager.Instance.UpdateGameState(Promotion);
     
        if (GameManager.Instance.state is Promotion) return true;
        
        //Calculate next possible movement of selected move to check that piece is checkmate or not.
        CalculateCheckKing(SelectedPiece, faction);
        AttackMove = new List<Vector2>();

        SetSelectedPiece(null);
        
        ReloadPiecesLeftInGame();
        return false;
    }

    #endregion



    
    /// <summary>
    /// It sets the piece to the tile's position and sets the tile's occupied piece to the piece
    /// </summary>
    /// <param name="piece">The piece that is being set to the tile</param>
    public void SetPiece(Piece piece)
    {
        
        //Drop piece out when set pos
        if (piece.occupiedTile)
        {
            piece.occupiedTile.occupiedPiece = null;
        }

        Vector2 newPos = transform.position;
        
        /* It sets the piece to the tile's position and sets the tile's occupied piece to the piece */
        piece.transform.position = newPos;
        piece.pos = newPos;
        occupiedPiece = piece;
        piece.occupiedTile = this;
        
        CursorManager.Instance.ResetCursor();
        
    }

}
