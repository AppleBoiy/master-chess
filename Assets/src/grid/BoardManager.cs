using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    #region params
    
    [SerializeField] private GridManager gridManager;

    private Dictionary<Vector2, Tile> _tiles;

    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        _tiles = gridManager.GetTiles();
    }
}
