using UnityEngine;
using UnityEngine.Serialization;

public class WhitePawnPromotionManager : PawnPromotionManager
{
    public static WhitePawnPromotionManager Instance;
    
   
    private void Awake()
    {
        Instance = this;
    }


    /// <inheritdoc />
    protected override void PromotionPieceOnTile(Piece newRoll)
    {
        Destroy(TempPiece.gameObject);
        PieceManager.SpawnPiece(PawnToPromotion.pos, newRoll, PieceManager.Instance.whiteParentPrefabs);
        
        //Close promotion scene
        ClosePromotionScene();
        
        //After promoted pawn update player turn
        UpdateGameAfterPromotion();
    }

    /// <inheritdoc />
    protected override void SpawnTempPiece(Piece pawnToPromotion, Piece tempPiece)
    {
        Destroy(pawnToPromotion.gameObject);
        PieceManager.SpawnPiece(PawnToPromotion.pos, tempPiece, PieceManager.Instance.blackParentPrefabs);
        
        Piece tempSpawnPiece = TileManager.Instance.GetTile(PawnToPromotion.pos).occupiedPiece;
        TempPiece = tempSpawnPiece;
    }

}
