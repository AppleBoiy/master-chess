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

    public static List<Vector2> CurrentPieceMove;

    #endregion


    public static void CalculateLegalMove(Piece piece)
    {
        var piecePosX =  piece.pos.x;
        var piecePosY =  piece.pos.y;
        LOG($"<color=green>Calculate legal move.. of {piece} at ({piecePosX}, {piecePosY})</color>");
        
        Vector2[][] legalMove = {};

        switch (piece.roll)
        {
            case Roll.King: 
                legalMove = new [] {KingWalk(piecePosX, piecePosY)};
                break;
            
            case Roll.Queen:
                var temp = new List<Vector2[]>();
                temp.AddRange(RookWalk(piecePosX, piecePosY));
                temp.AddRange(BishopWalk(piecePosX, piecePosY));
                legalMove = temp.ToArray();
                break;
            
            case Roll.Knight:
                legalMove = new [] {KnightWalk(piecePosX, piecePosY)};
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


        ShowLegalMove(legalMove, piece);
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

        var piecePos = piece.pos;
        
        switch (piece.faction)
        {
            case Faction.WHITE when piece.isFirstMove:
                move.Add(new Vector2(piecePos.x, piecePos.y+1));
                move.Add(new Vector2(piecePos.x, piecePos.y+2));
                break;
            
            case Faction.WHITE:
                move.Add(new Vector2(piecePos.x, piecePos.y+1));
                break;
            
            case Faction.BLACK when piece.isFirstMove:
                move.Add(new Vector2(piecePos.x, piecePos.y-1));
                move.Add(new Vector2(piecePos.x, piecePos.y-2));
                break;
            
            case Faction.BLACK:
                move.Add(new Vector2(piecePos.x, piecePos.y-1));
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }

        return move.ToArray();
    }

    #endregion

    private static void ShowLegalMove(IEnumerable<Vector2[]> legalMove, Piece piece)
    {
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;
        LOG("<color=red>Show Legal move</color>");
        var temp = new List<Vector2>{};
        
        foreach (var axis in legalMove)
        {
            
            foreach (var move in axis)
            {
                var tile = getTile(move);
                if (!tile) continue;
                
                if (tile.OccupiedPiece != null && tile.OccupiedPiece.faction == piece.faction)
                {   
                    if (piece.roll == Roll.Knight) continue;
                    break;
                }
                
                temp.Add(move);
                
                var highlight = getTile(move).transform.GetChild(2);
                highlight.GameObject().SetActive(true);
                highlight.GetComponentInChildren<SpriteRenderer>().color = Color.green;
            }
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
}
