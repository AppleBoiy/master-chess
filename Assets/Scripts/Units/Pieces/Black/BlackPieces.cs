using System;
using UnityEngine;

public class BlackPieces : Piece
{
    #region Promotion

    //Bottom last row on board
    private const float PromotionYPos = 0.0f;
    
    /// <inheritdoc />
    public override void CheckPawnPromotion()
    {
        if (roll is not Roll.Pawn) return;
        if (!(Math.Abs(pos.y - PromotionYPos) < 0.01f)) return;
        
        Debug.Log($"<color=black>Pawn {faction} can promotion now!</color>");
        BlackPawnPromotionManager.Instance.TimeToPromotion(this);
    }
    
    #endregion
}
