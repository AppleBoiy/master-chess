using System.Linq;
using UnityEngine;
using static IPiecesInGame;

public class BlackTeam : MonoBehaviour, IPiecesInGame
{
    public static Vector2 KingPos;
    
    
    /// <summary>
    /// > Find the king's position
    /// </summary>
    /// <returns>
    /// The position of the king.
    /// </returns>
    internal static void FindKing()
    {
        Piece[] king = IPiecesInGame.BlackPieces.Where(IsKing).ToArray();

        /* If there is no king, then return. */
        if (!king.Any()) return;
        KingPos = king[0].pos;
    }
    
}
