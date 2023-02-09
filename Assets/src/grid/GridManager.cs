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
    
    private Dictionary<string, Vector2> _whiteInitPos;
    private Dictionary<string, Vector2> _blackInitPos;

    #endregion

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        string rank = "ABCDEFGH";
        
        for (var column = 0; column < width; column++)
        {
            for (var row = 0; row < hight; row++)
            {
                var tile = Instantiate(tilePrefabs, new Vector3(column, row), identity);
                tile.name = $"{column + 1}{rank[row]}";
                var isOffset = (column % 2 == 0 && row % 2 != 0 ) || (column % 2 != 0 && row % 2 == 0);
                Vector2 pos = new Vector2(column, row);
                
                tile.Init(isOffset);

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

    public Dictionary<string, Vector2>[] InitPosPieces()
    {
        return new[] { _whiteInitPos, _blackInitPos };
    }
}
