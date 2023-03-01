using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPromotionManager : MonoBehaviour
{

    #region params

    [Header("Pawn promotion scene")] [SerializeField]
    private GameObject pawnPromotionMenu;
    
    public static PawnPromotionManager Instance;

    
    #endregion
    
    private void Awake()
    {
        Instance = this;
    }

    public void TimeToPromotion(Piece pawnToPromotion)
    {
        pawnPromotionMenu.SetActive(true);
    }

    
}
