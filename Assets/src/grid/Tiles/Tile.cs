using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    #region params

    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;

    private string _type;
    private Vector2 _pos;
    
    #endregion
    
    public void Init(bool isOffset, string type, Vector2 pos)
    {
        renderer.color = isOffset? baseColor : offsetColor;
        _type = type;
        _pos = pos;
    }

    public string TileType()
    {
        return _type;
    }

    public Vector2 GetPos()
    {
        return _pos;
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
