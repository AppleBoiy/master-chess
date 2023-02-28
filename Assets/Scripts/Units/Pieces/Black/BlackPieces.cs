using System;
using UnityEngine;

public class BlackPieces : Piece
{
    #region Promotion

    private const float PromotionYPos = 0.0f;
    
    public override void CheckPawnPromotion()
    {
        if (Math.Abs(pos.y - PromotionYPos) < 0.01f)
        {
            Debug.Log($"<color=black>Pawn {faction} can promotion now!</color>");

        } 
    }

    public override void PromotionPawn(Piece promotionToPiece)
    {
        PieceManager.SpawnPiece(pos, promotionToPiece, PieceManager.Instance.whiteParentPrefabs);
    }

    #endregion
}
