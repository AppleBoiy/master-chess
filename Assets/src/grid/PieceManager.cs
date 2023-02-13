using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PieceManager : MonoBehaviour
{

    #region params

    [Header("Player unit")]
    [SerializeField] private int whiteTeamPieces;
    [SerializeField] private int blackTeamPieces;

    [Space(3)] 
    [Header("Piece container")] 
    [SerializeField] private GameObject whiteParentPrefabs;
    
    public static PieceManager Instance;

    private List<ScriptablePiece> _pieces;
    
    #endregion

    private void Awake()
    {
        Instance = this;

        //found But not found WHAT!!!
        _pieces = Resources.LoadAll<ScriptablePiece>("GridScene").ToList();

        foreach (var piece in _pieces)
        {
            Debug.Log(piece);
        }
    }

    public void SpawnWhitePiece()
    {
        for (var i = 0; i < whiteTeamPieces; i++)
        {
            var randomPrefab = GetRandomUnit<Piece>(Faction.White);
            var spawnWhiteTeam = Instantiate(randomPrefab, whiteParentPrefabs.transform, true);
            var randomSpawnTile = GridManager.Instance.GetWhiteTeamSpawnTile();

            randomSpawnTile.SetPiece(spawnWhiteTeam);
            
        }
    }

    private T GetRandomUnit<T>(Faction faction) where T : Piece
    {
        return (T) _pieces.Where(
            u => u.faction == faction
            )
            .OrderBy(
                o => Random.value
                )
            .First()
            .piecePrefab;
    }
}
