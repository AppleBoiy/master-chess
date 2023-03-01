using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal interface IPiecesInGame
{
    public static List<Piece> WhitePieces;
    public static List<Piece> BlackPieces;
    
    //Reload Piece Left everytime piece is move out to another tile.
    public static void ReloadPiecesLeftInGame()
    {
        //Reset list to empty list
        WhitePieces = new List<Piece>();
        BlackPieces = new List<Piece>();
        
        //Start game do not calculate piece left
        if (GameManager.Instance.State is GameState.StartGame) return;

        //Get tile dictionary
        Dictionary<Vector2,Tile> instanceDictTiles = TileManager.Instance.DictTiles;
        
        Piece GetOccupiedPiece(KeyValuePair<Vector2, Tile> tilePos) => tilePos.Value.occupiedPiece;
        bool HasPieceOnTile(Piece occupiedPiece) => occupiedPiece;
        
        foreach (
            var occupiedPiece 
            in instanceDictTiles!.Select(GetOccupiedPiece).Where(HasPieceOnTile))
        {
            switch (occupiedPiece.faction)
            {
                case Faction.BLACK:
                    BlackPieces.Add(occupiedPiece);
                    break;
                
                case Faction.WHITE:
                    WhitePieces.Add(occupiedPiece);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        BlackTeam.FindKing();
        WhiteTeam.FindKing();
        
        MenuManager.Instance.ShowPieceLeft();
    }
    
}

