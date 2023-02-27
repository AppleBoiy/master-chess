using System.Collections.Generic;
using UnityEngine;

internal interface IPiecesInGame
{

    public static List<Piece> WhitePieces;
    public static List<Piece> BlackPieces;

    public static List<Piece> GetAllAlliance(Faction faction)
    {
        return faction == Faction.BLACK 
            ? BlackTeam.Instance.FindAllAlliance()
            : WhiteTeam.Instance.FindAllAlliance();
    }

    
    //Reload Piece Left everytime piece is move out to another tile.
    public static void ReloadPiecesLeftInGame()
    {
        
        if (GameManager.Instance.State == GameState.StartGame) return;
        
        var blackTeam = BlackTeam.Instance;
        var whiteTeam = WhiteTeam.Instance;
        
        var blackPieces = blackTeam.FindAllAlliance(); 
        var whitePieces = whiteTeam.FindAllAlliance();
        
        blackTeam.FindKing();
        whiteTeam.FindKing();

        WhitePieces = whitePieces;
        BlackPieces = blackPieces;

        Debug.Log($"{blackTeam} has {blackPieces.Count} left \n" +
                  $"<color=red>King</color> at {BlackTeam.KingPos}");
        
        Debug.Log($"{whiteTeam} has {whitePieces.Count} left \n" +
                  $"<color=red>King</color> at {WhiteTeam.KingPos}");
        
    }
    
}

