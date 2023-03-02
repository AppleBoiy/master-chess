using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ScriptableSelectPromotion : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    /// <summary>
    /// When mouse enter on choose piece btn show information if it.
    /// </summary>
    /// <param name="eventData"></param>
    public abstract void OnPointerEnter(PointerEventData eventData);

    
    /// <summary>
    /// When choose roll to promoted piece
    /// </summary>
    /// <param name="eventData"></param>
    public abstract void OnPointerDown(PointerEventData eventData);
}
