using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static IPieceStartingPos;
using Random = UnityEngine.Random;

public class PieceManager : MonoBehaviour
{

    #region params

    [Header("Player unit")]
    [Space(3)] 
    [Header("White Team Pieces")] 
    [SerializeField] private Piece pawn;
    
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

        // found But not found WHAT!!!
         _pieces = Resources.LoadAll<ScriptablePiece>("Pieces").ToList();

         foreach (var piece in _pieces)
         {
             Debug.Log(piece);
         }
    }

    #region Spawn Pieces

    public void SpawnWhitePieces()
    {

        var currentPos = IWhite.FirstPawn;

        do
        {
            var spawnWhitePiece = Instantiate(pawn, whiteParentPrefabs.transform, true);
            var spawnTileAtPos = TileManager.Instance.GetTile(currentPos);

            spawnWhitePiece.pos = spawnTileAtPos.GetPos();
            spawnTileAtPos.SetPiece(spawnWhitePiece);
            currentPos = new Vector2(currentPos.x, currentPos.y + 1);

        } while (currentPos != IWhite.LastPawn);
        
        
        // for (var i = 0; i < 1; i++)
        // {
        //     var randomPrefab = GetRandomUnit<WhitePieces>(Faction.WHITE);
        //     var spawnWhiteTeam = Instantiate(randomPrefab, whiteParentPrefabs.transform, true);
        //     var randomSpawnTile = TileManager.Instance.GetWhiteTeamSpawnTile();
        //
        //     spawnWhiteTeam.pos = randomSpawnTile.GetPos();
        //     
        //     randomSpawnTile.SetPiece(spawnWhiteTeam);
        //     
        //     Debug.Log($"<color=white>White</color> at {spawnWhiteTeam.pos}");
        //     
        // }
        
    }
    
    public void SpawnBlackPieces()
    {
        for (var i = 0; i < blackTeamPieces; i++)
        {
            var randomPrefab = GetRandomUnit<BlackPieces>(Faction.BLACK);
            var spawnBlackTeam = Instantiate(randomPrefab, blackParentPrefabs.transform, true);
            var randomSpawnTile = TileManager.Instance.GetBlackTeamSpawnTile();

            spawnBlackTeam.pos = randomSpawnTile.GetPos();
            
            randomSpawnTile.SetPiece(spawnBlackTeam);
            
            Debug.Log($"<color=black>Black</color> at {spawnBlackTeam.pos}");
        }
        
    }

    #endregion
    
    

    private T GetRandomUnit<T>(Faction faction) where T : Piece
    {
        return (T) _pieces.Where(
            u => u.Faction == faction
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
        MenuManager.Instance.ShowSelectedPiece(piece);
        Debug.Log("Show Selected Piece complete.");
        
    }
    
}
