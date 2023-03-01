using System.Linq;
using UnityEngine;

public class BlackTeam : MonoBehaviour, IPiecesInGame
{
    public static BlackTeam Instance;
    public static Vector2 KingPos;
    
    
    private void Awake()
    {
        Instance = this;
    }
    
    
    //Find King position on board
    internal static void FindKing()
    {
        //Introduce local method
        bool IsKing(Piece piece) => piece.roll is Roll.King;

        Piece piece = IPiecesInGame.BlackPieces.Where(IsKing).ToArray()[0];

        KingPos = piece.pos;
    }
}
