using UnityEngine;
using static Faction;
using static GameState;
using static PieceManager;


public class Tile : MonoBehaviour
{
    #region params

    [Header("Tile Uniqe")]
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;

    [Space(3)] 
    [Header("Tile attribute")] 
    [SerializeField] private bool isWalkable;
    
    private Vector2 _pos;

    public Piece OccupiedPiece;
    public bool Walkable => isWalkable && !OccupiedPiece;
    
    #endregion
    
    public void Init(bool isOffset, Vector2 pos)
    {
        renderer.color = isOffset? baseColor : offsetColor;
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
        Debug.Log($"On Mouse Down at {_pos}");

        switch (GameManager.Instance.State)
        {
            case BlackTurn when OccupiedPiece != null:
                Debug.Log("<color=yellow>Occupied Piece</color> is <color=red>not</color> <color=purple>null</color>");
            
                if (OccupiedPiece.faction == BLACK)
                {  
                    Debug.Log("<color=white>Black Piece</color> is on tile");
                
                    Instance.SetSelectedPiece(OccupiedPiece);
                }
                else
                {
                    Debug.Log("there is something on tile");

                    if (SelectedPiece == null) return;
                    var whitePiece = (WhitePieces) OccupiedPiece;
                    Destroy(whitePiece.gameObject);
                    
                    SetPiece(SelectedPiece);
                    Instance.SetSelectedPiece(null);
                    
                    GameManager.Instance.ChangeTurn();
                }
                break;
            
            case BlackTurn:
            {
                Debug.Log("Empty <color=green>Tile</color> is clicked!!");

                if (SelectedPiece == null) return;
                Debug.Log("Piece is prepare to move out!");
                
                SetPiece(SelectedPiece);
                
                Debug.Log("Set new tile to piece completed!!");
                
                Instance.SetSelectedPiece(null);
                
                GameManager.Instance.ChangeTurn();
                break;
            }
            
            case WhiteTurn when OccupiedPiece != null:
            {
                Debug.Log("<color=yellow>Occupied Piece</color> is <color=red>not</color> <color=purple>null</color>");
            
                if (OccupiedPiece.faction == WHITE)
                {  
                    Debug.Log("<color=white>White Piece</color> is on tile");
                
                    Instance.SetSelectedPiece(OccupiedPiece);
                }
                else
                {
                    Debug.Log("there is something on tile");

                    if (SelectedPiece == null) return;
                    
                    Debug.Log("White piece move to black piece");
                    var blackPiece = (BlackPieces) OccupiedPiece;
                    Destroy(blackPiece.gameObject);
                    
                    Debug.Log($"set <color=white>{SelectedPiece}</color> to {_pos}");
                    SetPiece(SelectedPiece);
                    Instance.SetSelectedPiece(null);
                    
                    GameManager.Instance.ChangeTurn();
                }

                break;
            }
            
            case WhiteTurn:
            {
                Debug.Log("Empty <color=green>Tile</color> is clicked!!");

                if (SelectedPiece == null) return;
                Debug.Log("Piece is prepare to move out!");
                
                SetPiece(SelectedPiece);
                
                Debug.Log("Set new tile to piece completed!!");
                
                PieceManager.Instance.SetSelectedPiece(null);
            
                GameManager.Instance.ChangeTurn();
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
        
        piece.transform.position = transform.position;
        OccupiedPiece = piece;
        piece.occupiedTile = this;
    }
    
}
