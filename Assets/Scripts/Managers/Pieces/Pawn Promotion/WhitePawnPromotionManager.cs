using UnityEngine;

public class WhitePawnPromotionManager : PawnPromotionManager
{
    public static WhitePawnPromotionManager Instance;
    
    [Space(3)] [Header("Temporary piece")]
    [SerializeField] private Piece tempPiece;
    
    private void Awake()
    {
        Instance = this;
    }


    /// <inheritdoc />
    protected override void SpawnTempPieceOnTile(Piece pawn)
    {
        Destroy(pawn.gameObject);
        
        PieceManager.SpawnPiece(pawn.pos, tempPiece, PieceManager.Instance.whiteParentPrefabs);
    }
    
    
}
