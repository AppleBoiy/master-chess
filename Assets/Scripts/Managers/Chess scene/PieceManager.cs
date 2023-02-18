using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using static IPieceStartingPos;
using Random = UnityEngine.Random;

public class PieceManager : MonoBehaviour
{

    #region params

    [Header("Player unit")]
    [Space(3)] 
    [Header("White Team Pieces")] 
    [SerializeField] private Piece pawn;
    [SerializeField] private Piece rook;
    [SerializeField] private Piece knight;
    [SerializeField] private Piece bishop;
    [SerializeField] private Piece queen;
    [SerializeField] private Piece king;
    
    [SerializeField] private int blackTeamPieces;

    [Space(3)] 
    [Header("Piece container")] 
    [SerializeField] private GameObject whiteParentPrefabs;
    [SerializeField] private GameObject blackParentPrefabs;
    
    public static PieceManager Instance;

    private List<ScriptablePiece> _pieces;
    
    [FormerlySerializedAs("SelectedPiece")]
    public Piece selectedPiece;
    
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
            SpawnPiece(currentPos, pawn, whiteParentPrefabs);
            
            currentPos = new Vector2(currentPos.x, currentPos.y + 1);

        } while (currentPos != IWhite.LastPawn);
        
        SpawnPiece(IWhite.King, king, whiteParentPrefabs);
        SpawnPiece(IWhite.Queen, queen, whiteParentPrefabs);
        
        SpawnPiece(IWhite.Bishop1, bishop, whiteParentPrefabs);
        SpawnPiece(IWhite.Bishop2, bishop, whiteParentPrefabs);
        SpawnPiece(IWhite.Rook1, rook, whiteParentPrefabs);
        SpawnPiece(IWhite.Rook2, rook, whiteParentPrefabs);
        SpawnPiece(IWhite.Knight1, knight, whiteParentPrefabs);
        SpawnPiece(IWhite.Knight2, knight, whiteParentPrefabs);
        
        
        
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
        
        selectedPiece = piece;

        Debug.Log($"{piece.name} is Selected");
        MenuManager.Instance.ShowSelectedPiece(piece);
        Debug.Log("Show Selected Piece complete.");
        
    }

    private static void SpawnPiece(Vector2 pos, Piece piece, GameObject parentPiece)
    {
        var spawnPiece = Instantiate(piece, parentPiece.transform, true);
        var spawnAtTile = TileManager.Instance.GetTile(pos);

        spawnPiece.pos = spawnAtTile.GetPos();
        spawnAtTile.SetPiece(spawnPiece);
        
    }

}
