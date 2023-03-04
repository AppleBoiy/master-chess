using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using static Faction;
using static GameState;
using static Piece;
using static PieceManager;


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
    /// > This function sets the sprite of the tile to either the base tile or the offset tile,
    /// depending on the value of the isOffset parameter
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
    /// When the mouse enters the tile, the tile's highlight is set to active and the tile's information
    /// is shown in the menu
    /// </summary>
    /// <returns>
    /// The return type is void, so nothing is being returned.
    /// </returns>
    private void OnMouseEnter()
    {
        //Promotion scene player can't interaction with board
        if (GameManager.Instance.state is Promotion or Setting) return;

        switch (occupiedPiece?.faction)
        {
            case Black:
                blackFrame.SetActive(true);
                break;
            case White:
                whiteFrame.SetActive(true);
                break;
        }
        
        highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
        
        blackFrame.SetActive(false);
        whiteFrame.SetActive(false);

    }

    
    /// <summary>
    /// This function is checking the state of the game and the piece that is on the tile. If the tile
    /// has a piece on it, then the function is checking if the piece on the tile is a black piece. If
    /// it is, then the piece is selected and its legal moves are calculated. If the piece on the tile
    /// is a white piece, then the piece is selected and its legal moves are calculated. If the tile is
    /// empty, then the function is checking if the selected piece is null or if the tile is not
    /// walkable. If either of these conditions are true, then the function returns. If the selected
    /// piece is a pawn, then the isFirstMove variable is set to false. The piece is then set on the
    /// tile and the turn is changed
    /// </summary>
    /// <returns>
    /// the legal moves of the piece.
    /// </returns>
    private void OnMouseDown()
    {
        MenuManager.ResetMove();
        
        Action changeTurn = GameManager.Instance.ChangeTurn;
        var instanceState = GameManager.Instance.state;
        
        /* The above code is a switch statement that is checking the state of the game and the piece
        that is on the tile. */
        switch (instanceState, occupiedPiece)
        {

            //tile has a piece on it
            case (BlackTurn, not null) :
                
                /* This is checking if the piece on the tile is a black piece. If it is, then the piece
                is selected and its legal moves are calculated. */
                if (occupiedPiece.faction is Black)
                {  
                    SetSelectedPiece(occupiedPiece);
                    ShowNormalMove(occupiedPiece);
                    
                    IPiecesInGame.ReloadPiecesLeftInGame();
                    
                }
                
                /* when the player clicks on a tile that has a piece on it. */
                else
                {
                    if (UnreachableTile()) return;
                    
                    //Attack (Destroy enemy game object)
                    var whitePiece = (WhitePieces) occupiedPiece;
                    Destroy(whitePiece.gameObject);
                    
                    if (MovePiece(Black)) return;
                    changeTurn();
                }
                break;
            
            //Click to empty tile
            case (BlackTurn, _) when UnreachableTile():
                return;
            case (BlackTurn, _) when MovePiece(Black):
                return;
            case (BlackTurn, _):
            {
                changeTurn();
                break;
            }
            
            /* This is checking if the piece on the tile is a white piece. If it is, then the piece
            is selected and its legal moves are calculated. */
            case (WhiteTurn, not null):
            {
                /* This is checking if the piece on the tile is a white piece. If it is, then the piece
                    is selected and its legal moves are calculated. */
                if (occupiedPiece.faction is White)
                {
                    SetSelectedPiece(occupiedPiece);
                    ShowNormalMove(occupiedPiece);
                    
                    IPiecesInGame.ReloadPiecesLeftInGame();
                }
                
                //Attack phase
                else
                {
                    if (UnreachableTile()) return;

                    //Attack (Destroy enemy game object) 
                    var blackPiece = (BlackPieces) occupiedPiece;
                    Destroy(blackPiece.gameObject);
                    
                    if (MovePiece(White)) return;
                    
                    changeTurn();
                }

                break;
            }
            
            /* This is checking if the piece on the tile is a white piece. If it is, then the piece
                        is selected and its legal moves are calculated. */
            case (WhiteTurn, _) when UnreachableTile():
                return;
            case (WhiteTurn, _) when MovePiece(White):
                return;
            case (WhiteTurn, _):
            {
                changeTurn();
                break;
            }
        }
        
        IPiecesInGame.ReloadPiecesLeftInGame();
    }

    private bool UnreachableTile()
    {
        /* This is checking if the selected piece is null or if the tile is not walkable.
                    If either of these
                    conditions are true, then the function returns. */
        if (SelectedPiece is null || !Walkable()) return true;

        /* This is checking if the selected piece is a pawn. If it is, then the isFirstMove
                    variable is set to false. */
        if (SelectedPiece.roll is Roll.Pawn) SelectedPiece.isFirstMove = false;
        return false;
    }

    private bool MovePiece(Faction faction)
    {
        SetPiece(SelectedPiece);

        //Check this pawn is ready to promotion or not
        SelectedPiece.CheckPawnPromotion();
        if (GameManager.Instance.state is Promotion) return true;
        
        //Calculate next possible movement of selected move to check that piece is checkmate or not.
        CalculateCheckKing(SelectedPiece, faction);
        AttackMove = new List<Vector2>();

        SetSelectedPiece(null);
        
        IPiecesInGame.ReloadPiecesLeftInGame();
        return false;
    }

    #endregion

    
    /// <summary>
    /// It sets the piece to the tile's position and sets the tile's occupied piece to the piece
    /// </summary>
    /// <param name="piece">The piece that is being set on the tile.</param>
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
