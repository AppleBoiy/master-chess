using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    #region params

    [Header("Initial game setting")] 
    [SerializeField] private new Transform camera;
    
    [Space(3)]
    [Header("Game object parents")]
    [SerializeField] private GameObject parentTiles;
    
    [Space(3)]
    [SerializeField] private Tile tile;

    [CanBeNull] 
    private readonly Dictionary<Vector2, Tile> _tiles = new();
    
    public static TileManager Instance;
    
    private const int Width = 8;
    private const int Height = 8;
    
    #endregion

    private void Awake()
    {
        Instance = this;
    }
    

    public void GenerateTile()
    {
        
        for (var column = 0; column < Width; column++)
        {
            for (var row = 0; row < Height; row++)
            {
                var instantiateTile = Instantiate(tile, new Vector3(column, row), quaternion.identity);
                var isOffset = (column % 2 == 0 && row % 2 != 0 ) || (column % 2 != 0 && row % 2 == 0);
                Vector2 pos = new Vector2(column, row);

                
                //set parent of tile
                instantiateTile.transform.parent = parentTiles.transform;
                instantiateTile.name = $"Tile at ({column}, {row})";

                instantiateTile.Init(isOffset, pos);

                _tiles![pos] = instantiateTile;
            }
        }
        
        camera.transform.position = new Vector3((float)Width / 2 - 0.5f,(float)Height/2 - 0.5f, -10 );
        
    }
    
    
    public Tile GetTile(Vector2 pos)
    {
        return _tiles!.TryGetValue(pos, value: out var getTile) ? getTile : null;
    }

    public Dictionary<Vector2, Tile> Tiles()
    {
        return _tiles;
    }

}
