using System;
using UnityEngine;
using static Faction;
using static GameState;
using static PieceManager;

public class Tile : MonoBehaviour
{
    #region Serialize Field

    [Header("Tile Uniqe")]
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;

    [Space(3)] 
    [Header("Tile attribute")] 
    [SerializeField] private bool isWalkable;
    
    #endregion

    #region params

    private static readonly MenuManager MenuManager = MenuManager.Instance;
    private static readonly GameManager GameManager = GameManager.Instance;
    private static readonly WhiteTeam WhiteTeam = WhiteTeam.Instance;
    private static readonly PieceManager PieceManager = Instance;
    private static readonly Action<object> LOG = Debug.Log;
    
    public Piece OccupiedPiece;
    public Vector2 pos;
    
    #endregion
    
    public void Init(bool isOffset, Vector2 pos)
    {
        renderer.color = isOffset? baseColor : offsetColor;
        this.pos = pos;
    }

  
    
    #region Mouse action

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
        MenuManager.ShowTileInfo(this);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
        MenuManager.ShowTileInfo(null);
    }

    private void OnMouseDown()
    {
        LOG($"On Mouse Down at {pos}");

        switch (GameManager.State)
        {
            case BlackTurn when OccupiedPiece != null:
                LOG("<color=yellow>Occupied Piece</color> is <color=red>not</color> <color=purple>null</color>");

                //Calculate legal move if not legal break
                if (!BlackTeam.Instance.CalculateLegalMove()) return;
                
                if (OccupiedPiece.faction == BLACK)
                {  
                    LOG("<color=white>Black Piece</color> is on tile");
                
                    PieceManager.SetSelectedPiece(OccupiedPiece);
                }
                else
                {
                    LOG("there is something on tile");

                    if (SelectedPiece == null) return;
                    var whitePiece = (WhitePieces) OccupiedPiece;
                    Destroy(whitePiece.gameObject);
                    
                    SetPiece(SelectedPiece);
                    PieceManager.SetSelectedPiece(null);
                    
                    GameManager.ChangeTurn();
                }
                break;
            
            case BlackTurn:
            {
                LOG("Empty <color=green>Tile</color> is clicked!!");

                if (SelectedPiece == null) return;
                LOG("Piece is prepare to move out!");
                
                SetPiece(SelectedPiece);
                
                LOG("Set new tile to piece completed!!");
                
                PieceManager.SetSelectedPiece(null);
                
                GameManager.ChangeTurn();
                break;
            }
            
            case WhiteTurn when OccupiedPiece != null:
            {
                LOG("<color=yellow>Occupied Piece</color> is <color=red>not</color> <color=purple>null</color>");
            
                //Calculate legal move if not legal break
                if (!WhiteTeam.CalculateLegalMove()) return;

                
                if (OccupiedPiece.faction == WHITE)
                {  
                    LOG("<color=white>White Piece</color> is on tile");
                
                    PieceManager.SetSelectedPiece(OccupiedPiece);
                }
                else
                {
                    LOG("there is something on tile");

                    if (SelectedPiece == null) return;
                    
                    LOG("White piece move to black piece");
                    var blackPiece = (BlackPieces) OccupiedPiece;
                    Destroy(blackPiece.gameObject);
                    
                    LOG($"set <color=white>{SelectedPiece}</color> to {pos}");
                    SetPiece(SelectedPiece);
                    PieceManager.SetSelectedPiece(null);
                    
                    GameManager.ChangeTurn();
                }

                break;
            }
            
            case WhiteTurn:
            {
                LOG("Empty <color=green>Tile</color> is clicked!!");

                if (SelectedPiece == null) return;
                LOG("Piece is prepare to move out!");
                
                SetPiece(SelectedPiece);
                
                LOG("Set new tile to piece completed!!");
                
                Instance.SetSelectedPiece(null);
            
                GameManager.ChangeTurn();
                break;
            }
            
            default:
                throw new ArgumentOutOfRangeException();
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
        
        piece.transform.position = transform.position;
        OccupiedPiece = piece;
        piece.occupiedTile = this;
    }
    
}
