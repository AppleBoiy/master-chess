using System;

public class WhitePieces : Piece
{
    #region Promotion

    //Top first row on board
    private const float PromotionYPos = 7.0f;

    /// <inheritdoc />
    public override bool CheckPawnPromotion()
    {
        if (roll is not Roll.Pawn) return false;
        if (!(Math.Abs(pos.y - PromotionYPos) < 0.01f)) return false;

        WhitePawnPromotionManager.Instance.TimeToPromotion(this);
        return true;
    }

    #endregion
}
