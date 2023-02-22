using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

using static Unity.Mathematics.quaternion;
using Random = UnityEngine.Random;

public class TileManager : MonoBehaviour
{

    #region params

    [Header("Initial game setting")] 
    [SerializeField] private int width;
    [SerializeField] private int hight;
    [SerializeField] private new Transform camera;
    
    [Space(3)]
    [Header("Game object parents")]
    [SerializeField] private GameObject parentTiles;

    [Space(3)]
    [SerializeField] private Tile Tile;

    [CanBeNull] private readonly Dictionary<Vector2, Tile> _tiles = new();

    public static TileManager Instance;
    
    #endregion

    private void Awake()
    {
        Instance = this;
    }
    

    public void GenerateTile()
    {

        for (var column = 0; column < width; column++)
        {
            for (var row = 0; row < hight; row++)
            {
                var tile = Instantiate(Tile, new Vector3(column, row), identity);
                var isOffset = (column % 2 == 0 && row % 2 != 0 ) || (column % 2 != 0 && row % 2 == 0);
                Vector2 pos = new Vector2(column, row);

                
                //set parent of tile
                tile.transform.parent = parentTiles.transform;
                tile.name = $"Tile at ({column}, {row})";

                tile.Init(isOffset, pos);

                _tiles![pos] = tile;
            }
        }
        
        camera.transform.position = new Vector3((float)width / 2 - 0.5f,(float)hight/2 - 0.5f, -10 );
        
    }
    
    
    public Tile GetTile(Vector2 pos)
    {
        return _tiles.TryGetValue(pos, out var tile) ? tile : null;
    }

    public Dictionary<Vector2, Tile> Tiles()
    {
        return _tiles;
    }

}
