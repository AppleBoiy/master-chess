using System.Collections.Generic;
using UnityEngine;
using static GameState;
using static Roll;

public class PieceManager : MonoBehaviour, IWhite, IBlack 
{

    #region params
    
    [Header("Player unit")]
    [Space(3)] 
    [Header("White Team Pieces")] 
    [SerializeField] private Piece whitePawn;
    [SerializeField] private Piece whiteRook;
    [SerializeField] private Piece whiteKnight;
    [SerializeField] private Piece whiteBishop;
    [SerializeField] private Piece whiteQueen;
    [SerializeField] private Piece whiteKing;

    [Space(3)] 
    [Header("Black Team Pieces")] 
    [SerializeField] private Piece blackPawn;
    [SerializeField] private Piece blackRook;
    [SerializeField] private Piece blackKnight;
    [SerializeField] private Piece blackBishop;
    [SerializeField] private Piece blackQueen;
    [SerializeField] private Piece blackKing;

    [Space(3)] 
    [Header("Piece container")] 
    [SerializeField] public GameObject whiteParentPrefabs;
    [SerializeField] public GameObject blackParentPrefabs;
    
    public static PieceManager Instance;
    public static Piece SelectedPiece;
    
    private List<Piece> _list;

    #endregion


    private void Awake()
    {
        Instance = this;
    }
    
    #region Spawn Pieces

    /// <summary>
    /// It spawns the white pieces on the board
    /// </summary>
    public void SpawnWhitePieces()
    {
        Vector2 currentPos = IWhite.FirstPawn;

        do
        {
            SpawnPiece(currentPos, whitePawn, whiteParentPrefabs);

            currentPos = new Vector2(currentPos.x + 1, currentPos.y);

        } while (currentPos.x <= IWhite.LastPawn.x);

        SpawnPiece(IWhite.King, whiteKing, whiteParentPrefabs);
        SpawnPiece(IWhite.Queen, whiteQueen, whiteParentPrefabs);

        SpawnPiece(IWhite.Bishop1, whiteBishop, whiteParentPrefabs);
        SpawnPiece(IWhite.Bishop2, whiteBishop, whiteParentPrefabs);
        SpawnPiece(IWhite.Rook1, whiteRook, whiteParentPrefabs);
        SpawnPiece(IWhite.Rook2, whiteRook, whiteParentPrefabs);
        SpawnPiece(IWhite.Knight1, whiteKnight, whiteParentPrefabs);
        SpawnPiece(IWhite.Knight2, whiteKnight, whiteParentPrefabs);
    }
    
    /// <summary>
    /// It spawns the black pieces.
    /// </summary>
    public void SpawnBlackPieces()
    {
        Vector2 currentPos = IBlack.FirstPawn;

        do
        {
            SpawnPiece(currentPos, blackPawn, blackParentPrefabs);

            currentPos = new Vector2(currentPos.x + 1, currentPos.y);

        } while (currentPos.x <= IBlack.LastPawn.x);

        SpawnPiece(IBlack.King, blackKing, blackParentPrefabs);
        SpawnPiece(IBlack.Queen, blackQueen, blackParentPrefabs);

        SpawnPiece(IBlack.Bishop1, blackBishop, blackParentPrefabs);
        SpawnPiece(IBlack.Bishop2, blackBishop, blackParentPrefabs);
        SpawnPiece(IBlack.Rook1, blackRook, blackParentPrefabs);
        SpawnPiece(IBlack.Rook2, blackRook, blackParentPrefabs);
        SpawnPiece(IBlack.Knight1, blackKnight, blackParentPrefabs);
        SpawnPiece(IBlack.Knight2, blackKnight, blackParentPrefabs);
        
    }
    
    
    
    /// <summary>
    /// This function spawns a piece at a given position, and sets the piece's position to the position
    /// of the tile it's spawned on
    /// </summary>
    /// <param name="pos">The position of the tile you want to spawn the piece at.</param>
    /// <param name="piece">The piece you want to spawn</param>
    /// <param name="parentPiece">The prefab of the piece you want to spawn.</param>
    public static void SpawnPiece(Vector2 pos, Piece piece, GameObject parentPiece)
    {
        Piece spawnPiece = Instantiate(piece, parentPiece.transform, true);
        Tile spawnAtTile = TileManager.Instance.GetTile(pos);

        spawnPiece.pos = spawnAtTile.GetPos();
        spawnPiece.isFirstMove = true;
        
        spawnAtTile.SetPiece(spawnPiece);
        
    }
    
    #endregion

    
    /// <summary>
    /// If the piece is null, set the selected piece to null and show the selected piece menu.
    /// Otherwise, set the selected piece to the piece and show the selected piece menu
    /// </summary>
    /// <param name="piece">The piece that is selected.</param>
    /// <returns>
    /// The piece that is being selected.
    /// </returns>
    public static void SetSelectedPiece(Piece piece)
    {
        if (piece is null)
        {
            SelectedPiece = null;
            MenuManager.Instance.ShowSelectedPiece(null);
            return;
        }
        if (piece.roll is not King && GameManager.Instance.state is CheckBlack or CheckWhite)
        {
            return;
        }
        
        SelectedPiece = piece;
        MenuManager.Instance.ShowSelectedPiece(piece);

    }

}
