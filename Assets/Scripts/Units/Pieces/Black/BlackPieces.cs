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
            Debug.Log($"<color={faction}>Pawn {faction} can promotion now!>/color>");
        } 
    }
    
    #endregion
}
