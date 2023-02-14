using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [SerializeField] private GameObject blackParentPrefabs;
    
    public static PieceManager Instance;

    private List<ScriptablePiece> _pieces;
    public Piece SelectedPiece;
    
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

    #region Spawn Pieces

    public void SpawnWhitePieces()
    {
        for (var i = 0; i < whiteTeamPieces; i++)
        {
            var randomPrefab = GetRandomUnit<WhitePieces>(Faction.White);
            var spawnWhiteTeam = Instantiate(randomPrefab, whiteParentPrefabs.transform, true);
            var randomSpawnTile = GridManager.Instance.GetWhiteTeamSpawnTile();

            spawnWhiteTeam.pos = randomSpawnTile.GetPos();
            
            randomSpawnTile.SetPiece(spawnWhiteTeam);
            
            Debug.Log($"<color=white>White</color> at {spawnWhiteTeam.pos}");
            
        }
        
        GridSceneManager.Instance.UpdateGameState(GridState.SpawnBlackPieces);
    }
    
    public void SpawnBlackPieces()
    {
        for (var i = 0; i < blackTeamPieces; i++)
        {
            var randomPrefab = GetRandomUnit<BlackPieces>(Faction.Black);
            var spawnBlackTeam = Instantiate(randomPrefab, blackParentPrefabs.transform, true);
            var randomSpawnTile = GridManager.Instance.GetBlackTeamSpawnTile();

            spawnBlackTeam.pos = randomSpawnTile.GetPos();
            
            randomSpawnTile.SetPiece(spawnBlackTeam);
            
            Debug.Log($"<color=black>Black</color> at {spawnBlackTeam.pos}");
        }
        
        GridSceneManager.Instance.UpdateGameState(GridState.WhitePlayerTurn);
    }

    #endregion
    
    

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

    public void SetSelectedPiece(Piece piece)
    {
        if (piece == null) return;
        
        SelectedPiece = piece;

        Debug.Log($"{piece.name} is Selected");
        GridMenuManager.Instance.ShowSelectedPiece(piece);
        Debug.Log("Show Selected Piece complete.");
        
    }
    
}
