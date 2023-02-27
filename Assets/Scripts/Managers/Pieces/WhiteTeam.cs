using System;
using System.Collections.Generic;
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

    /// <summary>
    /// Find all alliance is still on game
    /// </summary>
    /// <returns>List of alliance that are still on this game</returns>
    public List<Piece> FindAllAlliance()
    {
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;
        Func<int,Transform> getPieceGameObject = transform.GetChild;
        
        List<Piece> alliance = new List<Piece>();
        
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector2 piecePos = getPieceGameObject(i).transform.position;
            Piece piece = getTile(piecePos).occupiedPiece;
            
            alliance.Add(piece);
        }
        
        
        return alliance;
    }
    
    
    //Find King position on board
    internal void FindKing()
    {
        //Introduce local method
        bool IsKing(Piece piece) 
            => piece.roll is Roll.King;

        List<Piece> alliance = FindAllAlliance();

        Piece piece = alliance.Where(IsKing).ToArray()[0];

        KingPos = piece.pos;
    }
}

