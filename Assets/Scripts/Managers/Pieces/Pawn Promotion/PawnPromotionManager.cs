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

    public void TimeToPromotion(Piece pawnToPromotion)
    {
        //Store last game information (pawn to promotion, last game state)
        PawnToPromotion = pawnToPromotion;
        LastPlayer = GameManager.Instance.State;
        
        //Show pawn that promotion information
        promotionPosInfo.text = PawnToPromotion.pos.ToString();

        //Change game state to promotion
        GameManager.Instance.State = GameState.Promotion;
        
        //Destroy pawn and replace it with temporary piece before promotion
        SpawnTempPiece(pawnToPromotion, tempPiece);
        
        pawnPromotionMenu.SetActive(true);
    }

    public void SetSelectPromotionImg(Piece newPiece)
    {
        
        promotionImageHolder.GetComponent<Image>().sprite = newPiece.GetComponent<SpriteRenderer>().sprite;
        promotedRoll.text = newPiece.roll.ToString();

    }
    

    #endregion

    /// <summary>
    /// Spawn temporary piece on tile that prepare to spawn promoted pawn.
    /// </summary>
    /// <param name="pawn">Pawn that enter to promotion zone</param>
    /// <param name="tempPiece">Temporary piece replace on pawn before promotion pawn</param>
    protected abstract void PromotionPieceOnTile(Piece pawn);

    /// <summary>
    /// Spawn temporary piece and place in pawn that promoting.
    /// </summary>
    /// <param name="pawnToPromotion"></param>
    /// <param name="tempPiece"></param>
    protected abstract void SpawnTempPiece(Piece pawnToPromotion, Piece tempPiece);
    
}
