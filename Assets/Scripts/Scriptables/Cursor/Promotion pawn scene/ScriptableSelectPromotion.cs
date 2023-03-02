using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ScriptableSelectPromotion : MonoBehaviour, IPointerEnterHandler
{
    public abstract void OnPointerEnter(PointerEventData eventData);
}
