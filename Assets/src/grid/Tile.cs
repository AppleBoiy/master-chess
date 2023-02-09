using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{

    #region params

    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;

    private Vector2 _pos;


    #endregion


    public void Init(Vector2 pos, bool isOffset)
    {
        this._pos = pos;

        renderer.color = isOffset? baseColor : offsetColor; 
    }
    

    #region Mouse action

    private void OnMouseEnter()
    {
        highlight.SetActive(true);

    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }
    
    #endregion


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Mouse click at");
    }
}
