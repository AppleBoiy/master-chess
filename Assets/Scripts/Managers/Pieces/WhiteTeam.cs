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
            => piece.roll == Roll.King;

        List<Piece> alliance = FindAllAlliance();

        Piece piece = alliance.Where(IsKing).ToArray()[0];

        KingPos = piece.pos;
    }
}

