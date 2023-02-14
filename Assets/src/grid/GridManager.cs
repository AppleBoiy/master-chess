using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using static Unity.Mathematics.quaternion;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
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
    [Header("Path types")] 
    [SerializeField] private Tile path;
    [SerializeField] private Tile hole;

    [SerializeField] private int pathFaction;
    [SerializeField] private int holeFaction;
    
    private Dictionary<Vector2, Tile> _tiles;

    public static GridManager Instance;
    
    #endregion

    private void Awake()
    {
        Instance = this;
    }
    

    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();

        for (var column = 0; column < width; column++)
        {
            for (var row = 0; row < hight; row++)
            {
                var randomPath = Random.Range(0, pathFaction + holeFaction) < holeFaction? hole : path ;
                
                var tile = Instantiate(randomPath, new Vector3(column, row), identity);
                
                //set parent of tile
                tile.transform.parent = parentTiles.transform;
                
                tile.name = $"Tile at ({column}, {row})";
                string tileType =  (randomPath == hole)? "HOLE" : "PATH";

                var isOffset = (column % 2 == 0 && row % 2 != 0 ) || (column % 2 != 0 && row % 2 == 0);
                Vector2 pos = new Vector2(column, row);
                
                tile.Init(isOffset, tileType, pos);

                _tiles[pos] = tile;
            }
        }

        camera.transform.position = new Vector3((float)width / 2 - 0.5f,(float)hight/2 - 0.5f, -10 );
        
        GridSceneManager.Instance.UpdateGameState(GridState.SpawnWhitePieces);
    }

    public Tile GetWhiteTeamSpawnTile() 
    {
        return _tiles.Where(t 
                => t.Key.x < (float) width / 2
                && t.Value.Walkable
            ).OrderBy(t
                => Random.value
            ).First().Value;
    }
    
    public Tile GetBlackTeamSpawnTile() 
    {
        return _tiles.Where(t 
            => t.Key.x > (float) width / 2
               && t.Value.Walkable
        ).OrderBy(t
            => Random.value
        ).First().Value;
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
