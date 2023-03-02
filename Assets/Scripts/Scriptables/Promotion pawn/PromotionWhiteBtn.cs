using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PromotionWhiteBtn : ScriptableSelectPromotion
{
    [Space(3)] [Header("Promotion Image")] 
    [SerializeField] private Sprite pieceImg;
    
    public override void OnPointerEnter(PointerEventData eventData)
    {
        WhitePawnPromotionManager.Instance.SetSelectPromotionImg(pieceImg);
    }
}
