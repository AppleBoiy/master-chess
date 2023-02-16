using UnityEngine;
using static ChessGameManager;


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
        ChessMenuManager.Instance.ShowTileInfo(this);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
        ChessMenuManager.Instance.ShowTileInfo(null);
    }

    private void OnMouseDown()
    {
        Debug.Log($"On Mouse Down at {_pos}");
        
        if (Instance.State != GameState.WhiteTurn)
        {
            Debug.Log("<color=black>Black</color> Player Turn!");
            return;
        }

        // White Player turn
        if (OccupiedPiece != null)
        {
            Debug.Log("<color=yellow>Occupied Piece</color> is <color=red>not</color> <color=purple>null</color>");
            
            if (OccupiedPiece.Faction == Faction.WHITE)
            {  
                Debug.Log("<color=white>White Piece</color> is on tile");
                
                PieceManager.Instance.SetSelectedPiece(OccupiedPiece);
            }
            else
            {
                Debug.Log("there is something on tile");

                if (PieceManager.Instance.SelectedPiece == null) return;
                var blackPiece = (BlackPieces) OccupiedPiece;
                Destroy(blackPiece.gameObject);

                PieceManager.Instance.SetSelectedPiece(null);
            }
        }
        else
        {
            Debug.Log("Empty <color=green>Tile</color> is clicked!!");

            if (PieceManager.Instance.SelectedPiece == null) return;
            Debug.Log("Piece is prepare to move out!");
                
            SetPiece(PieceManager.Instance.SelectedPiece);
                
            Debug.Log("Set new tile to piece completed!!");
                
            PieceManager.Instance.SetSelectedPiece(null);
            
            // Instance.ChangeTurn();
        }
    }

    #endregion

    public void SetPiece(Piece piece)
    {
        if (piece.OccupiedTile)
        {
            piece.OccupiedTile.OccupiedPiece = null;
        }
        
        piece.transform.position = transform.position;
        OccupiedPiece = piece;
        piece.OccupiedTile = this;
    }
    
}
