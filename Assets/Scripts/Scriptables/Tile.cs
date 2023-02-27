using System;
using System.Linq;
using UnityEngine;
using static Faction;
using static GameState;
using static Piece;
using static PieceManager;
using static UnityEngine.Debug;


public class Tile : MonoBehaviour
{
    #region params

    [Header("Tile Unique")]
    [SerializeField] private Sprite baseTile;
    [SerializeField] private Sprite offsetTile;
    [SerializeField] private GameObject highlight;
    
    private Vector2 _pos;

    public Piece occupiedPiece;

    #endregion
    
    
    //Initialize Tile attribute
    public void Init(bool isOffset, Vector2 pos)
    {
        Transform child = transform.GetChild(1);
        SpriteRenderer componentInChildren = child.GetComponentInChildren<SpriteRenderer>();
        
        componentInChildren.sprite = isOffset ? baseTile : offsetTile;
        
        _pos = pos;
    }

    #region Getter

    
    //Has any tile in position in  walkable pos list
    private protected virtual bool Predicate(Vector2 pos) => _pos == pos;
    private bool Walkable() => CurrentPieceMove.Any(Predicate);

    

    public Vector2 GetPos() => _pos;
    
    #endregion
    
    #region Mouse action

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
    }

    private void OnMouseDown()
    {
        Log($"On Mouse Down at {_pos}");

        MenuManager.ResetMove();
        
        Action<Piece> selectedPiece = SetSelectedPiece;
        Action changeTurn = GameManager.Instance.ChangeTurn;
        var instanceState = GameManager.Instance.State;
        
        
        switch (instanceState)
        {

            //tile has a piece on it
            case BlackTurn when occupiedPiece is not null:
                
                if (occupiedPiece.faction is BLACK)
                {  
                    selectedPiece(occupiedPiece);
                    CalculateLegalMove(occupiedPiece);

                }
                else
                {
                    //Click on enemy
                    
                    if (SelectedPiece is null) return;
                    if (!Walkable()) return;
                    if (SelectedPiece.roll is Roll.Pawn) SelectedPiece.isFirstMove = false;
                    
                    var whitePiece = (WhitePieces) occupiedPiece;
                    Destroy(whitePiece.gameObject);
                    SetPiece(SelectedPiece);
                    
                    //Calculate next possible movement of selected move to check that piece is checkmate or not.
                    CalculateLegalMove(SelectedPiece);
                    
                    selectedPiece(null);

                    changeTurn();
                }

                break;
            
            //Click to empty tile
            case BlackTurn:
            {
                if (SelectedPiece is null) return;
                if (!Walkable()) return;
                if (SelectedPiece.roll is Roll.Pawn) SelectedPiece.isFirstMove = false;
                
                SetPiece(SelectedPiece);
                
                //Calculate next possible movement of selected move to check that piece is checkmate or not.
                CalculateLegalMove(SelectedPiece);
                
                selectedPiece(null);
                changeTurn();
                break;
            }
            
            //tile has a piece on it
            case WhiteTurn when occupiedPiece is not null:
            {
                if (occupiedPiece.faction is WHITE)
                {
                    selectedPiece(occupiedPiece);
                    CalculateLegalMove(occupiedPiece);
                }
                else
                {
                    //Click on enemy
                    if (SelectedPiece is null) return;
                    if (!Walkable()) return;
                    if (SelectedPiece.roll is Roll.Pawn) SelectedPiece.isFirstMove = false;

                    var blackPiece = (BlackPieces) occupiedPiece;
                    Destroy(blackPiece.gameObject);
                    SetPiece(SelectedPiece);
                    
                    //Calculate next possible movement of selected move to check that piece is checkmate or not.
                    CalculateLegalMove(SelectedPiece);
                    
                    selectedPiece(null);
                    changeTurn();
                }

                break;
            }
            
            //click on empty tile
            case WhiteTurn:
            {
                if (SelectedPiece is null) return;
                if (!Walkable()) return;
                if (SelectedPiece.roll is Roll.Pawn) SelectedPiece.isFirstMove = false;
                
                SetPiece(SelectedPiece);
                
                //Calculate next possible movement of selected move to check that piece is checkmate or not.
                CalculateLegalMove(SelectedPiece);

                selectedPiece(null);
                changeTurn();
                break;
            }
            
            default:
                Log(false);
                break;
        }
    }


    #endregion

    public void SetPiece(Piece piece)
    {
        
        //Drop piece out when set pos
        if (piece.occupiedTile)
        {
            piece.occupiedTile.occupiedPiece = null;
        }

        Vector2 newPos = transform.position;
        
        piece.transform.position = newPos;
        piece.pos = newPos;
        occupiedPiece = piece;
        piece.occupiedTile = this;
        
        CursorManager.Instance.ResetCursor();
     
        IPiecesInGame.ReloadPiecesLeftInGame();
    }

    
}
