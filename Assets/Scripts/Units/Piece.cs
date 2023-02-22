using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Piece : MonoBehaviour
{
    #region params
    
    [FormerlySerializedAs("OccupiedTile")] 
    public Tile occupiedTile;
    
    [FormerlySerializedAs("Faction")] 
    public Faction faction;
    
    [FormerlySerializedAs("Roll")] 
    public Roll roll;
    public bool isFirstMove;
    
    public Vector2 pos;
    private static readonly Action<object> LOG = Debug.Log;

    #endregion
    
    public static IEnumerable<Vector2> CalculateLegalMove(Piece piece)
    {

        LOG($"<color=green>Calculate legal move.. of {piece} at ({piece.pos.x}, {piece.pos.y})</color>");
        
        Vector2[] legalMove = {};

        switch (piece.roll)
        {
            case Roll.King: 
                legalMove = KingWalk(piece.pos.x, piece.pos.y);
                break;
            
            case Roll.Queen:
                break;
            
            case Roll.Knight:
                break;
            
            case Roll.Rook:
                break;
            
            case Roll.Bishop:
                break;
            
            case Roll.Pawn:
                legalMove = PawnWalk(piece);
                break;
            
            case Roll.Piece:
                break;
            
            default:
                return legalMove;
        }


        LOG($"Total legal move is {legalMove.Length}: {legalMove} ");
        ShowLegalMove(legalMove);
        
        return legalMove;
    }


    #region Piece move

    private static Vector2[] KingWalk(float x, float y)
    {

        LOG("Calculate king move!");
        
        return new[]
        {
            new Vector2(x - 1, y + 1),
            new Vector2(x - 1, y - 1),

            new Vector2(x + 1, y + 1),
            new Vector2(x + 1, y - 1),

            new Vector2(x - 1, y),
            new Vector2(x + 1, y),

            new Vector2(x, y + 1),
            new Vector2(x, y - 1)
        };
    }

    private static Vector2[] PawnWalk(Piece piece)
    {

        var move = new List<Vector2>{};
        
        switch (piece.faction)
        {
            case Faction.WHITE when piece.isFirstMove:
                move.Add(new Vector2(piece.pos.x, piece.pos.y+1));
                move.Add(new Vector2(piece.pos.x, piece.pos.y+2));
                break;
            
            case Faction.WHITE:
                move.Add(new Vector2(piece.pos.x, piece.pos.y+1));
                break;
            
            case Faction.BLACK when piece.isFirstMove:
                move.Add(new Vector2(piece.pos.x, piece.pos.y-1));
                move.Add(new Vector2(piece.pos.x, piece.pos.y-2));
                break;
            
            case Faction.BLACK:
                move.Add(new Vector2(piece.pos.x, piece.pos.y-1));
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }

        return move.ToArray();
    }

    #endregion

    private static void ShowLegalMove(IEnumerable<Vector2> legalMove)
    {
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;
        
        foreach (var move in legalMove)
        {
            if (!getTile(move)) continue;
            
            LOG(getTile(move));
            
            var highlight = getTile(move).transform.GetChild(2);
            highlight.GameObject().SetActive(true);
            highlight.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }
    }
}
