using UnityEngine;
using UnityEngine.EventSystems;

public class PromotionBlackBtn: ScriptableSelectPromotion
{
    [Space(3)] [Header("Promotion Image")] 
    [SerializeField] private Piece promotedPiece;


    /// <inheritdoc />
    public override void OnPointerEnter(PointerEventData eventData)
    {
        BlackPawnPromotionManager.Instance.SetSelectPromotionImg(promotedPiece);
    }

    /// <inheritdoc />
    public override void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(name);
    }
}