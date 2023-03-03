using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Unity.VisualScripting;
using static GameState;

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
        _turnDialog.text = GameManager.Instance.state.ToString();
    }

    /// <summary>
    /// If the piece is null, hide the selected piece. Otherwise, set the text of the selected piece to
    /// the roll of the piece and show the selected piece
    /// </summary>
    /// <param name="piece">The piece that is selected.</param>
    /// <returns>
    /// The selected piece is being returned.
    /// </returns>
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
    /// If the tile is null, hide the tile info, piece on tile, and piece info. Otherwise, show the tile
    /// info, piece on tile, and piece info
    /// </summary>
    /// <param name="tile">The tile that the mouse is hovering over.</param>
    /// <returns>
    /// The tile that the mouse is hovering over.
    /// </returns>
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

    /// <summary>
    /// When the user clicks the "White" button, the game state is updated to "WhiteTurn" and the start
    /// game holder is disabled
    /// </summary>
    public void SelectWhitePlayer()
    {
        GameManager.Instance.UpdateGameState(WhiteTurn);
        startGameHolder.SetActive(false);
    }

    /// <summary>
    /// > This function is called when the user selects the black player
    /// </summary>
    public void SelectBlackPlayer()
    {
        GameManager.Instance.UpdateGameState(BlackTurn);
        startGameHolder.SetActive(false);
    }

    /// <summary>
    /// This function loops through all the tiles in the game and sets the move indicator to false
    /// </summary>
    public static void ResetMove()
    {
        foreach (var tile in TileManager.Instance.Tiles().Values)
        {
            tile.transform.GetChild(2).GameObject().SetActive(false);
        }
    }


    /// <summary>
    /// It updates the UI text to show the number of pieces left on the board and the position of the
    /// kings
    /// </summary>
    /// <returns>
    /// The method is returning the number of pieces left on the board.
    /// </returns>
    public void ShowPieceLeft()
    {
        blackPieceLeftInfo.text = IPiecesInGame.BlackPieces.Count.ToString();
        whitePieceLeftInfo.text = IPiecesInGame.WhitePieces.Count.ToString();

        if (GameManager.Instance.state is StartGame) return;
        
        blackKingPosInfo.text = BlackTeam.KingPos.ToString();
        whiteKingPosInfo.text = WhiteTeam.KingPos.ToString();
    }
}
