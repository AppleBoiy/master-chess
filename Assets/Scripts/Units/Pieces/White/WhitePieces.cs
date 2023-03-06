using System;
using UnityEngine;

public class WhitePieces : Piece
{
    #region Promotion

    //Top first row on board
    private const float PromotionYPos = 7.0f;

    /// <inheritdoc />
    public override void CheckPawnPromotion()
    {
        if (roll is not Roll.Pawn) return;
        if (!(Math.Abs(pos.y - PromotionYPos) < 0.01f)) return;

        Debug.Log($"<color=white>Pawn {faction} can promotion now!</color>");
        WhitePawnPromotionManager.Instance.TimeToPromotion(this);

    }

    #endregion
}
