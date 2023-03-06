using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
using static IPiecesInGame;
using static Piece;
using static PieceManager;


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
    

    /// <summary>
    /// When the user clicks on a promotion button, the PromotionPieceOnTile function is called with the
    /// prefab of the piece that the user wants to promote to
    /// </summary>
    private void Start()
    {
        selectQueenBtn?.onClick.AddListener(delegate { PromotionPieceOnTile(queenPrefab); });
        selectKnightBtn?.onClick.AddListener(delegate { PromotionPieceOnTile(knightPrefab); });
        selectBishopBtn?.onClick.AddListener(delegate { PromotionPieceOnTile(bishopPrefab); });
        selectRookBtn?.onClick.AddListener(delegate { PromotionPieceOnTile(rookPrefabs); });
    }

    #region Instance method

   
    /// <summary>
    /// > When a pawn reaches the end of the board, the game state changes to promotion, the pawn is
    /// destroyed and replaced with a temporary piece, and the pawn promotion menu is shown
    /// </summary>
    /// <param name="pawnToPromotion">pawnToPromotion - The pawn that is being promoted.</param>
    public void TimeToPromotion(Piece pawnToPromotion)
    {
        
        //Store last game information (pawn to promotion, last game state)
        PawnToPromotion = pawnToPromotion;
        LastPlayer = State;
        
        
        //Show pawn that promotion information
        promotionPosInfo.text = PawnToPromotion.pos.ToString();

        //Destroy pawn and replace it with temporary piece before promotion
        SpawnTempPiece(pawnToPromotion, tempPiece);
        
        pawnPromotionMenu.SetActive(true);
    }

   
    
    /// <summary>
    /// This function is called when a player selects a piece to promote to. It sets the image of the
    /// piece to promote to in the promotion menu
    /// </summary>
    /// <param name="newPiece">The piece that is being promoted to.</param>
    public void SetSelectPromotionImg(Piece newPiece)
    {
        /* Setting the image of the piece to promote to in the promotion menu */
        promotionImageHolder.GetComponent<Image>().sprite = newPiece.GetComponent<SpriteRenderer>().sprite;
        promotedRoll.text = newPiece.roll.ToString();
    }
    

    #endregion


    /// <summary>
    /// It updates the game after a pawn has been promoted
    /// </summary>
    internal static void UpdateGameAfterPromotion()
    {
        //Reset attack move
        AttackMove = new List<Vector2>();
        
        ReloadPiecesLeftInGame();
        SetSelectedPiece(null);
        UpdateGameState(LastPlayer);
        ChangeTurn();

        
    }

   
    /// <summary>
    /// This function is called when a pawn reaches the other side of the board
    /// </summary>
    /// <param name="pawn">The piece that is being promoted.</param>
    protected abstract void PromotionPieceOnTile(Piece pawn);


    /// <summary>
    /// "This function is called when a pawn reaches the end of the board and needs to be promoted to a
    /// different piece."
    /// 
    /// The function is abstract because it needs to be implemented in the derived class
    /// </summary>
    /// <param name="pawnToPromotion">The piece that is being promoted.</param>
    /// <param name="tempPiece"></param>
    protected abstract void SpawnTempPiece(Piece pawnToPromotion, Piece tempPiece);

    /// <summary>
    /// This function is called when the player clicks on the "Cancel" button in the pawn promotion
    /// menu. It simply deactivates the pawn promotion menu
    /// </summary>
    internal void ClosePromotionScene()
    {
        pawnPromotionMenu.SetActive(false);
    }
}
