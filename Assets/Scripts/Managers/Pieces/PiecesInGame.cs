using System.Collections.Generic;
using UnityEngine;

internal interface IPiecesInGame
{
    public static List<Piece> WhitePieces;
    public static List<Piece> BlackPieces;
    
    //Reload Piece Left everytime piece is move out to another tile.
    public static void ReloadPiecesLeftInGame()
    {
        

        WhitePieces = new List<Piece>();
        BlackPieces = new List<Piece>();
        if (GameManager.Instance.State == GameState.StartGame) return;
        foreach (KeyValuePair<Vector2,Tile> tilePos in TileManager.Instance.DictTiles!)
        {
            Piece occupiedPiece = tilePos.Value.occupiedPiece;
            if (!occupiedPiece) continue;
            
            if (occupiedPiece.faction is Faction.BLACK) BlackPieces.Add(occupiedPiece);
            if (occupiedPiece.faction is Faction.WHITE) WhitePieces.Add(occupiedPiece);
        }

        var blackTeam = BlackTeam.Instance;
        var whiteTeam = WhiteTeam.Instance;
        
        blackTeam.FindKing();
        whiteTeam.FindKing();
        
        MenuManager.Instance.ShowPieceLeft();
    }
    
}

