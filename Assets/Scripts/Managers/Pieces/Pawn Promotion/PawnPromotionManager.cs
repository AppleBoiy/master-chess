using TMPro;
using UnityEngine;
using UnityEngine.UI;


public abstract class PawnPromotionManager : MonoBehaviour
{

    #region params

    [Header("Pawn promotion scene")] 
    [SerializeField] private GameObject pawnPromotionMenu;

    [Space(3)] [Header("Pawn Info")] 
    [SerializeField] private TMP_Text promotionPosInfo;


    [Space(3)] [Header("Select piece Button")] 
    [SerializeField] private Button selectQueenBtn;
    [SerializeField] private Button selectRookBtn;
    [SerializeField] private Button selectBishopBtn;
    [SerializeField] private Button selectKnightBtn;

    [Space(3)] [Header("Promotion piece prefabs")] 
    [SerializeField] private Piece queenPrefab;
    [SerializeField] private Piece rookPrefabs;
    [SerializeField] private Piece bishopPrefab;
    [SerializeField] private Piece knightPrefab;
    
    [Header("Selected To Promotion Roll Info")] 
    [SerializeField] private GameObject promotionImageHolder;
    [SerializeField] private TMP_Text promotedRoll;
    
    [Space(3)] [Header("Temporary piece")]
    [SerializeField] private Piece tempPiece;
    
    internal static GameState LastPlayer;
    internal static Piece PawnToPromotion;
    internal static Piece TempPiece;
    
    #endregion
    

    private void Start()
    {
        selectQueenBtn?.onClick.AddListener(delegate { PromotionPieceOnTile(queenPrefab); });
        selectKnightBtn?.onClick.AddListener(delegate { PromotionPieceOnTile(knightPrefab); });
        selectBishopBtn?.onClick.AddListener(delegate { PromotionPieceOnTile(bishopPrefab); });
        selectRookBtn?.onClick.AddListener(delegate { PromotionPieceOnTile(rookPrefabs); });
    }

    #region Instance method

   /// <summary>
   /// > This function is called when a pawn reaches the end of the board and is ready to be promoted.
   /// It stores the pawn to be promoted and the last game state, shows the pawn's position on the
   /// promotion menu, changes the game state to promotion, and spawns a temporary piece in place of the
   /// pawn
   /// </summary>
   /// <param name="pawnToPromotion">The pawn that is being promoted.</param>
    public void TimeToPromotion(Piece pawnToPromotion)
    {
        //Store last game information (pawn to promotion, last game state)
        PawnToPromotion = pawnToPromotion;
        LastPlayer = GameManager.Instance.state;
        
        //Show pawn that promotion information
        promotionPosInfo.text = PawnToPromotion.pos.ToString();

        //Change game state to promotion
        GameManager.Instance.state = GameState.Promotion;
        
        //Destroy pawn and replace it with temporary piece before promotion
        SpawnTempPiece(pawnToPromotion, tempPiece);
        
        pawnPromotionMenu.SetActive(true);
    }

   
    /// <summary>
    /// This function is called when a player selects a piece to promote to. It sets the promotion image
    /// holder to the sprite of the piece that was selected
    /// </summary>
    /// <param name="newPiece">The piece that is being promoted to.</param>
    public void SetSelectPromotionImg(Piece newPiece)
    {
        promotionImageHolder.GetComponent<Image>().sprite = newPiece.GetComponent<SpriteRenderer>().sprite;
        promotedRoll.text = newPiece.roll.ToString();
    }
    

    #endregion


    
    /// <summary>
    /// It updates the game after a promotion has occurred
    /// </summary>
    internal static void UpdateGameAfterPromotion()
    {
        IPiecesInGame.ReloadPiecesLeftInGame();
        PieceManager.SetSelectedPiece(null);
        GameManager.Instance.UpdateGameState(LastPlayer);
        GameManager.Instance.ChangeTurn();
        
        PieceManager.SetSelectedPiece(null);
    }

    /// <summary>
    /// > This function is called when a pawn reaches the end of the board and needs to be promoted to a
    /// different piece
    /// </summary>
    /// <param name="pawn">The piece that is being promoted.</param>
    protected abstract void PromotionPieceOnTile(Piece pawn);

   
    /// <summary>
    /// > Spawns a temporary piece to be used for promotion
    /// </summary>
    /// <param name="pawnToPromotion">The piece that is being promoted.</param>
    /// <param name="tempPiece"></param>
    protected abstract void SpawnTempPiece(Piece pawnToPromotion, Piece tempPiece);

    internal void ClosePromotionScene()
    {
        pawnPromotionMenu.SetActive(false);
    }
}
