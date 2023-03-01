using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PawnPromotionManager : MonoBehaviour
{

    #region params

    [Header("Pawn promotion scene")] 
    [SerializeField] private GameObject pawnPromotionMenu;

    [Space(3)] [Header("Pawn Info")] 
    [SerializeField] private TMP_Text promotionPosInfo;
    
    public static PawnPromotionManager Instance;
    public static Piece PawnToPromotion;

    
    #endregion
    
    private void Awake()
    {
        Instance = this;
    }

    public void TimeToPromotion(Piece pawnToPromotion)
    {
        PawnToPromotion = pawnToPromotion;
        promotionPosInfo.text = PawnToPromotion.pos.ToString();
        
        pawnPromotionMenu.SetActive(true);
    }

    
}
