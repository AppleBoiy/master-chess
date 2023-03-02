using System.Linq;
using UnityEngine;
using static IPiecesInGame;

public class WhiteTeam : MonoBehaviour, IPiecesInGame
{
    
    public static WhiteTeam Instance;
    public static Vector2 KingPos;
    
    private void Awake()
    {
        Instance = this;
    }
    
    
    //Find King position on board
    internal static void FindKing()
    {
        Piece[] king = IPiecesInGame.BlackPieces.Where(IsKing).ToArray();

        if (!king.Any()) return;
        KingPos = king[0].pos;
    }
    
}

