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
    public readonly Dictionary<Vector2, Tile> DictTiles = new();
    public static TileManager Instance;
    
    private const int Width = 8;
    private const int Height = 8;
    
    #endregion

    private void Awake()
    {
        Instance = this;
    }
    

    /// <summary>
    /// For each column and row, instantiate a tile, set its parent, and add it to the dictionary
    /// </summary>
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

                DictTiles![pos] = instantiateTile;
            }
        }
        
        /* Setting the camera position to the center of the board. */
        camera.transform.position = new Vector3((float)Width / 2 - 0.5f,(float)Height/2 - 0.5f, -10 );
        
    }
    
    
    /// <summary>
    /// If the dictionary has a key of the position, then return the value of that key. 
    /// 
    /// If the dictionary does not have a key of the position, then return null. 
    /// 
    /// The above function is used in the following function:
    /// </summary>
    /// <param name="pos">The position of the tile.</param>
    /// <returns>
    /// The tile at the position.
    /// </returns>
    public Tile GetTile(Vector2 pos)
    {
        return DictTiles!.TryGetValue(pos, value: out Tile getTile) ? getTile : null;
    }

    /// <summary>
    /// It returns a dictionary of all the tiles in the map
    /// </summary>
    /// <returns>
    /// A dictionary of tiles.
    /// </returns>
    internal Dictionary<Vector2, Tile> Tiles() => DictTiles;
}
