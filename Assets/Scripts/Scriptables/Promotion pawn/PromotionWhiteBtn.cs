using UnityEngine;
using UnityEngine.EventSystems;

public class PromotionWhiteBtn : ScriptableSelectPromotion
{
    [Space(3)] [Header("Promotion Image")] 
    [SerializeField] private Piece promotedPiece;


    /// <inheritdoc />
    public override void OnPointerEnter(PointerEventData eventData)
    {
        WhitePawnPromotionManager.Instance.SetSelectPromotionImg(promotedPiece);
    }

    /// <inheritdoc />
    public override void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(name);
    }
}
