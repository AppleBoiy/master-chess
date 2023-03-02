using UnityEngine;

public class BlackPawnPromotionManager : PawnPromotionManager
{
    public static BlackPawnPromotionManager Instance;

    [Space(3)] [Header("Temporary piece")]
    [SerializeField] private Piece tempPiece;
    
    private void Awake()
    {
        Instance = this;
    }

    public override void SpawnTempPieceOnTile(Piece pawn)
    {

        Destroy(pawn.gameObject);
        PieceManager.SpawnPiece(pawn.pos, tempPiece, PieceManager.Instance.whiteParentPrefabs);
        
    }
}
