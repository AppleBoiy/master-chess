using System;
using System.Collections.Generic;
using UnityEngine;

using static Unity.Mathematics.quaternion;

public class GridManager : MonoBehaviour
{

    #region params
    
    [SerializeField] private int width, hight;

    [SerializeField] private Tile tilePrefabs;
    [SerializeField] private new Transform camera;

    private Dictionary<Vector2, Tile> _tiles;

    #endregion

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < hight; y++)
            {
                var tile = Instantiate(tilePrefabs, new Vector3(x, y), identity);
                tile.name = $"Tile {x} {y}";
                var isOffset = (x % 2 == 0 && y % 2 != 0 ) || (x % 2 != 0 && y % 2 == 0);
                Vector2 pos = new Vector2(x, y);
                
                tile.Init(pos, isOffset);

                _tiles[pos] = tile;
            }
        }

        camera.transform.position = new Vector3((float)width / 2 - 0.5f,(float)hight/2 - 0.5f, -10 );
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        return _tiles.TryGetValue(pos, out var tile) ? tile : null;
    }

    public Dictionary<Vector2, Tile> GetTiles()
    {
        return _tiles;
    }
}
