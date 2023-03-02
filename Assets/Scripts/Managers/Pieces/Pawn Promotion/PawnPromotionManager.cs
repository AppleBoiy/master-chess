using TMPro;
using Unity.VisualScripting;
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

    private static GameState _lastPlayer;
    private static Piece _pawnToPromotion;
    
    #endregion
    

    private void Start()
    {
        selectQueenBtn?.onClick.AddListener(delegate { SelectedPromotion(queenPrefab); });
        selectKnightBtn?.onClick.AddListener(delegate { SelectedPromotion(knightPrefab); });
        selectBishopBtn?.onClick.AddListener(delegate { SelectedPromotion(bishopPrefab); });
        selectRookBtn?.onClick.AddListener(delegate { SelectedPromotion(rookPrefabs); });
    }

    #region Instance method

    public void TimeToPromotion(Piece pawnToPromotion)
    {
        //Store last game information (pawn to promotion, last game state)
        _pawnToPromotion = pawnToPromotion;
        _lastPlayer = GameManager.Instance.State;
        
        //Show pawn that promotion information
        promotionPosInfo.text = _pawnToPromotion.pos.ToString();

        //Change game state to promotion
        GameManager.Instance.State = GameState.Promotion;
        
        SpawnTempPieceOnTile(pawnToPromotion);
        
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
    protected abstract void SpawnTempPieceOnTile(Piece pawn);

    private static void SelectedPromotion(Piece promotionRoll)
    {
        Debug.Log(promotionRoll.roll);
    }
}
