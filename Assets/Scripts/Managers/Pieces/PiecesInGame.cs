using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Faction;

internal interface IPiecesInGame
{
    public static List<Piece> WhitePieces;
    public static List<Piece> BlackPieces;
    
    
   /// <summary>
   /// It gets all the pieces that are in the game and adds them to the list of pieces that are left in
   /// the game
   /// </summary>
   /// <returns>
   /// The piece that is on the tile.
   /// </returns>
    public static void ReloadPiecesLeftInGame()
    {
        /* Resetting the list to an empty list. */
        WhitePieces = new List<Piece>();
        BlackPieces = new List<Piece>();
        
        /* Checking if the game is in the start state. If it is, then it will return. */
        if (GameManager.Instance.state is GameState.StartGame) return;

        /* Getting the dictionary of tiles from the TileManager. */
        Dictionary<Vector2,Tile> instanceDictTiles = TileManager.Instance.DictTiles;

        /* Getting all the pieces that are in the game. */
        IEnumerable<Piece> piecesInGame = instanceDictTiles!.Select(GetOccupiedPiece).Where(HasPieceOnTile);
        
        foreach (Piece piece in piecesInGame)
        {
            switch (piece.faction)
            {
                case Black:
                    BlackPieces.Add(piece);
                    break;
                
                
                case White:
                    WhitePieces.Add(piece);
                    break;
            }
        }
        
        BlackTeam.FindKing();
        WhiteTeam.FindKing();
        
        MenuManager.Instance.ShowPieceLeft();
    }

   
    #region MyRegion

   
   /// <summary>
   /// It takes a KeyValuePai as a parameter, and returns the occupiedPiece of the Tile
   /// </summary>
   /// <param name="tilePos">The tile position and tile object.</param>
   /// <returns>
   /// The occupied piece on the tile.
   /// </returns>
    private static Piece GetOccupiedPiece(KeyValuePair<Vector2, Tile> tilePos)
    {
        return tilePos.Value.occupiedPiece;
    }

    
    /// <summary>
    /// If the piece is not null, then return true
    /// </summary>
    /// <param name="occupiedPiece">The piece that is being moved.</param>
    /// <returns>
    /// A boolean value.
    /// </returns>
    private static bool HasPieceOnTile(Piece occupiedPiece)
    {
        return occupiedPiece;
    }

    
    /// <summary>
    /// IsKing returns true if the piece's roll is a king.
    /// </summary>
    /// <param name="piece">The piece you want to check</param>
    /// <returns>
    /// A boolean value.
    /// </returns>
    internal static bool IsKing(Piece piece)
    {
        return piece.roll is Roll.King;
    }

    #endregion
    
}

