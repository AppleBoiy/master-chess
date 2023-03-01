using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Unity.VisualScripting;

public class MenuManager : MonoBehaviour
{
    
    #region params

    [Header("Start Scene")] 
    [SerializeField] private GameObject startGameHolder;
    

    [Space(3)] [Header("Panel")] 
    [SerializeField] private GameObject selectedPiece;
    [SerializeField] private GameObject tileInfo;
    [SerializeField] private GameObject pieceOnTile;
    [SerializeField] private GameObject playerTurn;

    [Space(3)] [Header("Game Info")] 
    [SerializeField] private GameObject pieceInfo;

    [Space(3)] [Header("Piece left")] 
    [SerializeField] private TMP_Text blackPieceLeftInfo;
    [SerializeField] private TMP_Text whitePieceLeftInfo;
    [SerializeField] private TMP_Text blackKingPosInfo;
    [SerializeField] private TMP_Text whiteKingPosInfo;
    
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
        
        if (piece == null)
        {
            selectedPiece.SetActive(false);
            return;
        }
        
        selectedPiece.GetComponentInChildren<TMP_Text>().text = piece.roll.ToString() ;
        selectedPiece.SetActive(true);
    }

    /// <summary>
    /// Show information of tile etc. this tile has occupied piece or not. this tile is empty tile or this position is not tile
    /// </summary>
    /// <param name="tile">Tile to get info</param>
    public void ShowTileInfo(Tile tile)
    {
        
        //This position isn't tile
        if (tile == null)
        {
            tileInfo.SetActive(false);
            pieceOnTile.SetActive(false);       
            pieceInfo.SetActive(false);
            return;
        }
        
        //Show tile name (Position)
        tileInfo.GetComponentInChildren<TMP_Text>().text = tile.name;
        tileInfo.SetActive(true);
        
        //Show occupied piece on it
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

    public void ShowPieceLeft()
    {
        blackPieceLeftInfo.text = IPiecesInGame.BlackPieces.Count.ToString();
        whitePieceLeftInfo.text = IPiecesInGame.WhitePieces.Count.ToString();

        if (GameManager.Instance.State is GameState.StartGame) return;
        
        blackKingPosInfo.text = BlackTeam.KingPos.ToString();
        whiteKingPosInfo.text = WhiteTeam.KingPos.ToString();
    }
}
