using UnityEngine;

public class Tile : MonoBehaviour
{
    #region params

    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;

    #endregion
    
    public void Init(bool isOffset)
    {
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
