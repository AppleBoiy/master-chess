using System;
using System.Collections.Generic;
using System.Linq;
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

    public static List<Vector2> CurrentPieceMove;
    
    #endregion


    public static void CalculateLegalMove(Piece piece)
    {
        float piecePosX =  piece.pos.x;
        float piecePosY =  piece.pos.y;
        Faction pFaction = piece.faction;
        
        LOG($"<color=green>Calculate legal move.. of {piece} at ({piecePosX}, {piecePosY})</color>");
        
        Vector2[][] legalMove = {};

        switch (piece.roll)
        {
            case Roll.King: 
                legalMove = new [] {KingWalk(piecePosX, piecePosY, pFaction)};
                break;
            
            case Roll.Queen:
                var temp = new List<Vector2[]>();
                temp.AddRange(RookWalk(piecePosX, piecePosY));
                temp.AddRange(BishopWalk(piecePosX, piecePosY));
                legalMove = temp.ToArray();
                break;
            
            case Roll.Knight:
                legalMove = new [] {KnightWalk(piecePosX, piecePosY, pFaction)};
                break;
            
            case Roll.Rook:
                legalMove = RookWalk(piecePosX, piecePosY);
                break;
            
            case Roll.Bishop:
                legalMove = BishopWalk(piecePosX, piecePosY);
                break;
            
            case Roll.Pawn:
                legalMove = new []{PawnWalk(piece)};
                break;
            
            case Roll.Piece:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }


        ShowLegalMove(legalMove);
    }


    #region Piece move

    private static Vector2[][] BishopWalk(float x, float y)
    {
        var topRightMove  = new List<Vector2>();
        var downRightMove =  new List<Vector2>();
        var topLeftMove =  new List<Vector2>();
        var downLeftMove =  new List<Vector2>();
            
        for (var i = 1; i < 9; i++)
        {
            topRightMove.Add( new Vector2(x + i, y + i));
            downRightMove.Add(new Vector2(x + i, y - i));
            topLeftMove.Add(new Vector2(x - i, y + i));
            downLeftMove.Add(new Vector2(x - i, y - i));
            
        }

        return new[]
        {
            topLeftMove.ToArray(),
            downLeftMove.ToArray(),
            topRightMove.ToArray(),
            downRightMove.ToArray()
        };
    }

    private static Vector2[][] RookWalk(float x, float y)
    {
        var topMove = new List<Vector2>();
        var downMove = new List<Vector2>();
        var leftMove = new List<Vector2>();
        var rightMove = new List<Vector2>();

        for (var i = 1; i < 9; i++)
        {
            topMove.Add(new Vector2(x, y + i));
            downMove.Add(new Vector2(x, y - i));
            leftMove.Add(new Vector2(x - i, y));
            rightMove.Add(new Vector2(x + i, y));

        }

        return new[]
        {
            topMove.ToArray(),
            downMove.ToArray(),
            leftMove.ToArray(),
            rightMove.ToArray()
        };
    }

    private static Vector2[] KnightWalk(float x, float y, Faction faction)
    {
        LOG("Calculate knight move!");

        var temp = new List<Vector2> {
            new(x + 2, y + 1),
            new(x + 1, y + 2),
            new(x - 1, y + 2),
            new(x - 2, y + 1),
            
            new(x - 2, y - 1),
            new(x - 1, y - 2),
            new(x + 1, y - 2),
            new(x + 2, y - 1)
        };

        var legalMove = new List<Vector2>();
        
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;

        foreach (var pos in from pos in temp
                 let tile = getTile(pos)
                 where tile && (tile.OccupiedPiece == null || tile.OccupiedPiece.faction != faction)
                 select pos)
        {
            LOG(pos);
            legalMove.Add(pos);
        }

        return legalMove.ToArray();
    }

    private static Vector2[] KingWalk(float x, float y, Faction faction)
    {

        LOG("Calculate king move!");
        
        var temp = new List<Vector2>
        {
            new(x - 1, y + 1),
            new(x - 1, y - 1),

            new(x + 1, y + 1),
            new(x + 1, y - 1),

            new(x - 1, y),
            new(x + 1, y),

            new(x, y + 1),
            new(x, y - 1)
        };

        var legalMove = new List<Vector2>();
        
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;

        foreach (var pos in from pos in temp
                 let tile = getTile(pos)
                 where tile && (tile.OccupiedPiece == null || tile.OccupiedPiece.faction != faction)
                 select pos)
        {
            LOG(pos);
            legalMove.Add(pos);
        }
        
        
        return legalMove.ToArray();
    }

    private static Vector2[] PawnWalk(Piece piece)
    {
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;
        var move = new List<Vector2>{};

        
        var piecePosX = piece.pos.x;
        var piecePosY = piece.pos.y;
        
        switch (piece.faction)
        {
            case Faction.WHITE when piece.isFirstMove:
                move.Add(new Vector2(piecePosX, piecePosY + 1));
                move.Add(new Vector2(piecePosX, piecePosY + 2));
                move.Add(new Vector2(piecePosX - 1, piecePosY + 1));
                move.Add(new Vector2(piecePosX + 1, piecePosY + 1));
                break;
            
            case Faction.WHITE:
                move.Add(new Vector2(piecePosX, piecePosY + 1));
                move.Add(new Vector2(piecePosX - 1, piecePosY + 1));
                move.Add(new Vector2(piecePosX + 1, piecePosY + 1));
                break;
            
            case Faction.BLACK when piece.isFirstMove:
                move.Add(new Vector2(piecePosX, piecePosY - 1));
                move.Add(new Vector2(piecePosX, piecePosY - 2));
                move.Add(new Vector2(piecePosX - 1, piecePosY - 1));
                move.Add(new Vector2(piecePosX + 1, piecePosY - 1));
                break;
            
            case Faction.BLACK:
                move.Add(new Vector2(piecePosX, piecePosY - 1));
                move.Add(new Vector2(piecePosX - 1, piecePosY - 1));
                move.Add(new Vector2(piecePosX + 1, piecePosY - 1));
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }

        var temp = new List<Vector2>();
        
        foreach (var pos in move)
        {
            var tile = getTile(pos);
            if (!tile) continue;
            
            // front straight tile is empty tile
            if (tile.OccupiedPiece == null && tile.GetPos().x - piece.pos.x == 0)
                temp.Add(pos);
            
            // font left and right is not empty and occupiedPiece faction is not same 
            if (tile.OccupiedPiece != null && tile.OccupiedPiece.faction != piece.faction && tile.GetPos().x - piece.pos.x != 0)
                temp.Add(pos);
            
        }
        return temp.ToArray();
    }

    #endregion

    private static void ShowLegalMove(IEnumerable<Vector2[]> legalMove)
    {
        LOG("<color=red>Show Legal move</color>");
        
        var temp = new List<Vector2>{};
        
        foreach (var axis in legalMove)
        {
            temp.AddRange(axis.Select(ShowHighlight));
        }

        CurrentPieceMove = temp;

    }

    private void OnDestroy()
    {
        Debug.Log($"{this} will be destroyed!!");
        if (roll != Roll.King) return;
        Debug.Log("This game was END!!");
        GameManager.Instance.UpdateGameState(GameState.END);
    }

    private static Vector2 ShowHighlight(Vector2 move)
    {
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;
        
        Transform highlight = getTile(move).transform.GetChild(2);
                
        highlight.GameObject().SetActive(true);
        highlight.GetComponentInChildren<SpriteRenderer>().color = Color.green;

        return move;
    }
}
