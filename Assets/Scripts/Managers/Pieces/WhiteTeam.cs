using System.Linq;
using UnityEngine;

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
        //Introduce local method

        bool IsKing(Piece piece) 
            => piece.roll == Roll.King;
        Piece piece = IPiecesInGame.WhitePieces.Where(IsKing).ToArray()[0];

        KingPos = piece.pos;
    }
}

