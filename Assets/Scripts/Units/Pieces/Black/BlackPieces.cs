using static System.Math;

public class BlackPieces : Piece
{
    #region Promotion

    //Bottom last row on board
    private const float PromotionYPos = 0.0f;
    
    /// <inheritdoc />
    public override bool CheckPawnPromotion()
    {
        if (roll is not Roll.Pawn) return false;
        if (!(Abs(pos.y - PromotionYPos) < 0.01f)) return false;
        
        BlackPawnPromotionManager.Instance.TimeToPromotion(this);
        return true;
    }
    
    #endregion
}
