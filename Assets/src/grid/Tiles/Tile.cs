using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    #region params

    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;

    private string _type;
    
    #endregion
    
    public void Init(bool isOffset, string type)
    {
        renderer.color = isOffset? baseColor : offsetColor;
        _type = type;
    }

    public string TileType()
    {
        return _type;
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
