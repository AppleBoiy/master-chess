using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    
    #region params

    [Header("Start Scene")] 
    [SerializeField] private GameObject startGameHolder;
    

    [Space(3)] 
    [Header("Panel")] 
    [SerializeField] private GameObject selectedPiece;
    [SerializeField] private GameObject tileInfo;
    [SerializeField] private GameObject pieceOnTile;
    [SerializeField] private GameObject playerTurn;

    [Space(3)] [Header("Game Info")] 
    [SerializeField] private GameObject pieceInfo;
    
    public static MenuManager Instance;
    private TMP_Text _turnDialog;
    
    #endregion

    private void Start()
    {
        _turnDialog = playerTurn.GetComponentInChildren<TMP_Text>();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        
        _turnDialog.text = GameManager.Instance.State.ToString();
    }

    public void ShowSelectedPiece(Piece piece)
    {
        
        if (piece is null)
        {
            selectedPiece.SetActive(false);
            return;
        }
        
        selectedPiece.GetComponentInChildren<TMP_Text>().text = piece.roll.ToString() ;
        selectedPiece.SetActive(true);
    }

    public void ShowTileInfo(Tile tile)
    {
        
        if (tile is null)
        {
            tileInfo.SetActive(false);
            pieceOnTile.SetActive(false);       
            pieceInfo.SetActive(false);
            return;
        }
        
        tileInfo.GetComponentInChildren<TMP_Text>().text = tile.name;
        tileInfo.SetActive(true);
        
        if (!tile.occupiedPiece) return;
        pieceOnTile.GetComponentInChildren<TMP_Text>().text = $"{tile.occupiedPiece.faction}  {tile.occupiedPiece.roll}";
        pieceOnTile.SetActive(true);

        pieceInfo.SetActive(true);
        pieceInfo.GetComponent<Image>().sprite = tile.occupiedPiece.GetComponent<SpriteRenderer>().sprite;

    }


    public void SelectWhitePlayer()
    {
        GameManager.Instance.UpdateGameState(GameState.WhiteTurn);
        startGameHolder.SetActive(false);
    }

    public void SelectBlackPlayer()
    {
        GameManager.Instance.UpdateGameState(GameState.BlackTurn);
        startGameHolder.SetActive(false);
    }

    public static void ResetMove()
    {
        foreach (var tile in TileManager.Instance.Tiles().Values)
        {
            tile.transform.GetChild(2).GameObject().SetActive(false);
        }
    }
}
