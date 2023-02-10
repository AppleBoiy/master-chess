using System.Collections.Generic;
using UnityEngine;

using static Unity.Mathematics.quaternion;

public class GridManager : MonoBehaviour
{

    #region params
    
    [SerializeField] private int width, hight;
    [SerializeField] private new Transform camera;

    [SerializeField] private Tile path, hole;
    
    private Dictionary<Vector2, Tile> _tiles;

    #endregion

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();

        for (var column = 0; column < width; column++)
        {
            for (var row = 0; row < hight; row++)
            {
                var randomPath = Random.Range(0, 6) == 3? hole : path ;
                
                var tile = Instantiate(randomPath, new Vector3(column, row), identity);
                tile.name = $"Tile at ({column}, {row})";
                string tileType =  (randomPath == hole)? "HOLE" : "PATH";

                var isOffset = (column % 2 == 0 && row % 2 != 0 ) || (column % 2 != 0 && row % 2 == 0);
                Vector2 pos = new Vector2(column, row);
                
                tile.Init(isOffset, tileType, pos);

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
