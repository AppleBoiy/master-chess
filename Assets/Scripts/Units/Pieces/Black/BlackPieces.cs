using System;
using UnityEngine;

public class BlackPieces : Piece
{
    #region Promotion

    //Bottom last row on board
    private const float PromotionYPos = 0.0f;
    
    /// <summary>
    /// Check pawn moved to promotion zone yet? if already popout promotion scene
    /// </summary>
    public override void CheckPawnPromotion()
    {
        if (roll is not Roll.Pawn) return;
        if (!(Math.Abs(pos.y - PromotionYPos) < 0.01f)) return;
        
        Debug.Log($"<color=black>Pawn {faction} can promotion now!</color>");
        BlackPawnPromotionManager.Instance.TimeToPromotion(this);
    }

    public override void PromotionPawn(Piece promotionToPiece)
    {
        PieceManager.SpawnPiece(pos, promotionToPiece, PieceManager.Instance.whiteParentPrefabs);
    }

    #endregion
}
