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
    
    
    /// <summary>
    /// > Find the king's position by finding the first black piece that is a king
    /// </summary>
    /// <returns>
    /// The position of the king.
    /// </returns>
    internal static void FindKing()
    {
        Piece[] king = IPiecesInGame.BlackPieces.Where(IsKing).ToArray();

        /* If there is no king, return. */
        if (!king.Any()) return;
        KingPos = king[0].pos;
    }
    
}

