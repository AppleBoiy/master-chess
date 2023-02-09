using UnityEngine;

public class Tile : MonoBehaviour
{
    #region params

    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;

    private Vector2 _pos;


    #endregion
    
    public void Init(Vector2 pos, bool isOffset)
    {
        _pos = pos;

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
    
}
