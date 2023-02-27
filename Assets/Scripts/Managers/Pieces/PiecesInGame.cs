using System.Collections.Generic;

internal interface IPiecesInGame
{

    public static List<Piece> WhitePieces;
    public static List<Piece> BlackPieces;

    public static List<Piece> GetAllAlliance(Faction faction)
    {
        BlackTeam blackTeam = BlackTeam.Instance;
        WhiteTeam whiteTeam = WhiteTeam.Instance;

        blackTeam.FindKing();
        whiteTeam.FindKing();
        
        return faction == Faction.BLACK 
            ? blackTeam.FindAllAlliance() 
            : whiteTeam.FindAllAlliance();
    }
    
    
}

