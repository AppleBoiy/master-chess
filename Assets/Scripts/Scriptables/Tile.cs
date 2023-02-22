using System;
using UnityEngine;
using static Faction;
using static GameState;
using static PieceManager;
using static UnityEngine.Debug;


public class Tile : MonoBehaviour
{
    #region params

    [Header("Tile Uniqe")]
    [SerializeField] private Sprite baseTile;
    [SerializeField] private Sprite offsetTile;
    [SerializeField] private GameObject highlight;

    [Space(3)] 
    [Header("Tile attribute")] 
    [SerializeField] private bool isWalkable;
    
    private Vector2 _pos;

    public Piece OccupiedPiece;

    #endregion
    
    public void Init(bool isOffset, Vector2 pos)
    {
        transform.GetChild(1).GetComponentInChildren<SpriteRenderer>().sprite = isOffset ? baseTile : offsetTile;
        
        _pos = pos;
    }

    #region Getter
    public Vector2 GetPos()
    {
        return _pos;
    }
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
        
        MenuManager.Instance.ResetMove();
        
        Action<Piece> selectedPiece = Instance.SetSelectedPiece;
        Action changeTurn = GameManager.Instance.ChangeTurn;
        var instanceState = GameManager.Instance.State;
        
        switch (instanceState)
        {
            //tile has a piece on it
            case BlackTurn when OccupiedPiece != null:
                Log("Occupied Piece is not null");
            
                if (OccupiedPiece.faction == BLACK)
                {  
                    Log("<color=white>Black Piece</color> is on tile");
                
                    selectedPiece(OccupiedPiece);
                    
                    Log($"<color=red>{OccupiedPiece}</color>");
                    Piece.CalculateLegalMove(OccupiedPiece);

                }
                else
                {
                    Log("there is something on tile");
                    if (SelectedPiece == null) return;
                    
                    var whitePiece = (WhitePieces) OccupiedPiece;
                    Destroy(whitePiece.gameObject);
                    
                    SetPiece(SelectedPiece);
                    selectedPiece(null);
                    
                    changeTurn();
                }
                break;
            
            //Click to empty tile
            case BlackTurn:
            {
                Log("Empty Tile is clicked!!");

                if (SelectedPiece == null) return;
                Log("Piece is prepare to move out!");
                
                SetPiece(SelectedPiece);
                
                Log("Set new tile to piece completed!!");
                
                selectedPiece(null);
                
                changeTurn();
                break;
            }
            
            //tile has a piece on it
            case WhiteTurn when OccupiedPiece != null:
            {
                Log("Occupied Piece is not null");
            
                if (OccupiedPiece.faction == WHITE)
                {  
                    Log("<color=white>White Piece</color> is on tile");
                
                    selectedPiece(OccupiedPiece);

                    Log($"<color=red>{OccupiedPiece}</color>");

                    Piece.CalculateLegalMove(OccupiedPiece);
                }
                else
                {
                    Log("there is something on tile");

                    if (SelectedPiece == null) return;
                    
                    Log("White piece move to black piece");
                    var blackPiece = (BlackPieces) OccupiedPiece;
                    Destroy(blackPiece.gameObject);
                    
                    Log($"set <color=white>{SelectedPiece}</color> to {_pos}");
                    
                    SetPiece(SelectedPiece);
                    
                    selectedPiece(null);
                    
                    changeTurn();
                }

                break;
            }
            
            //click on empty tile
            case WhiteTurn:
            {
                Log("Empty <color=green>Tile</color> is clicked!!");

                if (SelectedPiece == null) return;
                Log("Piece is prepare to move out!");
                
                SetPiece(SelectedPiece);
                
                Log("Set new tile to piece completed!!");
                
                selectedPiece(null);
            
                changeTurn();
                break;
            }
        }
    }


    #endregion

    public void SetPiece(Piece piece)
    {
        
        //Drop piece out when set pos
        if (piece.occupiedTile)
        {
            piece.occupiedTile.OccupiedPiece = null;
        }

        Vector2 newPos = transform.position;
        
        piece.transform.position = newPos;
        piece.pos = newPos;
        OccupiedPiece = piece;
        piece.occupiedTile = this;
        
    }
    
}
