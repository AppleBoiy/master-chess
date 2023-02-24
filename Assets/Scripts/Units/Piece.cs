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

    public static Vector2[] CurrentPieceMove;

    #endregion
    
    public static void CalculateLegalMove(Piece piece)
    {
        Vector2 piecePos = piece.pos;
        
        LOG($"<color=green>Calculate legal move.. of {piece} at ({piecePos.x}, {piecePos.y})</color>");
        
        Vector2[] legalMove = {};

        switch (piece.roll)
        {
            case Roll.King: 
                legalMove = KingWalk(piecePos.x, piecePos.y);
                break;
            
            case Roll.Queen:
                var temp = new List<Vector2>();
                temp.AddRange(RookWalk(piecePos.x, piecePos.y));
                temp.AddRange(BishopWalk(piecePos.x, piecePos.y));
                legalMove = temp.ToArray();
                break;
            
            case Roll.Knight:
                legalMove = KnightWalk(piecePos.x, piecePos.y);
                break;
            
            case Roll.Rook:
                legalMove = RookWalk(piecePos.x, piecePos.y);
                break;
            
            case Roll.Bishop:
                legalMove = BishopWalk(piecePos.x, piecePos.y);
                break;
            
            case Roll.Pawn:
                legalMove = PawnWalk(piece);
                break;
            
            case Roll.Piece:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }


        LOG($"Total legal move is {legalMove.Length}: {legalMove} ");
        ShowLegalMove(legalMove, piece);
        
        CurrentPieceMove =  legalMove;
    }


    #region Piece move

    private static Vector2[] BishopWalk(float x, float y)
    {
        var legalWalk = new List<Vector2>();

        for (var i = 1; i < 9; i++)
        {
            var topRightMove = new Vector2(x + i, y + i);
            var downRightMove = new Vector2(x + i, y - i);
            var topLeftMove = new Vector2(x - i, y + i);
            var downLeftMove = new Vector2(x - i, y - i);
            
            legalWalk.AddRange(new []{topLeftMove, topRightMove, downLeftMove, downRightMove});
        }

        return legalWalk.ToArray();
    }

    private static Vector2[] RookWalk(float x, float y)
    {
        var legalWalk = new List<Vector2>();

        for (var i = 1; i < 9; i++)
        {
            var topMove = new Vector2(x, y + i);
            var downMove = new Vector2(x, y - i);
            var leftMove = new Vector2(x - i, y);
            var rightMove = new Vector2(x + i, y);
            
            legalWalk.AddRange(new []{topMove, downMove, leftMove, rightMove});
        }

        return legalWalk.ToArray();
    }

    private static Vector2[] KnightWalk(float x, float y)
    {
        LOG("Calculate knight move!");

        return new[]
        {
            new Vector2(x + 2, y + 1),
            new Vector2(x + 1, y + 2),
            new Vector2(x - 1, y + 2),
            new Vector2(x - 2, y + 1),
            
            new Vector2(x - 2, y - 1),
            new Vector2(x - 1, y - 2),
            new Vector2(x + 1, y - 2),
            new Vector2(x + 2, y - 1)
        };
    }

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

    private static void ShowLegalMove(IEnumerable<Vector2> legalMove, Piece piece)
    {
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;
        
        foreach (var move in legalMove)
        {
            if (!getTile(move)) continue;

            if (getTile(move).OccupiedPiece.faction == piece.faction) continue;
            
            LOG($"<color=yellow>{getTile(move).OccupiedPiece.faction} master {piece.faction}</color>");

            LOG(getTile(move));
            
            var highlight = getTile(move).transform.GetChild(2);
            highlight.GameObject().SetActive(true);
            highlight.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }
    }

    private void OnDestroy()
    {
        Debug.Log($"{this} will be destroyed!!");
        if (roll != Roll.King) return;
        Debug.Log("This game was END!!");
        GameManager.Instance.UpdateGameState(GameState.END);
    }
}
