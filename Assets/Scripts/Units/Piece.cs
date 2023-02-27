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
    
    private static readonly Action<object> LOG = Debug.Log;

    public static List<Vector2> CurrentPieceMove;
    public static List<Vector2> AttackMove;

    public Vector2 pos;
    
    #endregion
    
    #region Calculate Move

    /// <summary>
    /// Calculate to find legal move of selected piece
    /// </summary>
    /// <param name="piece">Selected piece</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void CalculateLegalMove(Piece piece)
    {
        float piecePosX =  piece.pos.x;
        float piecePosY =  piece.pos.y;
        Faction pFaction = piece.faction;
        Vector2[] legalMove = {};

        //Legal move depends on piece roll
        switch (piece.roll)
        {
            case Roll.King: 
                legalMove = KingWalk(piecePosX, piecePosY, pFaction);
                break;
            
            case Roll.Queen:
                //Queen move is  Rook and Bishop Legal move (Both of them)
                var temp = new List<Vector2>();
                temp.AddRange(RookWalk(piecePosX, piecePosY, pFaction));
                temp.AddRange(BishopWalk(piecePosX, piecePosY, pFaction));
                legalMove = temp.ToArray();
                
                break;
            
            case Roll.Knight:
                legalMove = KnightWalk(piecePosX, piecePosY, pFaction);
                break;
            
            case Roll.Rook:
                legalMove = RookWalk(piecePosX, piecePosY, pFaction);
                break;
            
            case Roll.Bishop:
                legalMove = BishopWalk(piecePosX, piecePosY, pFaction);
                break;
            
            case Roll.Pawn:
                legalMove = PawnWalk(piece);
                break;
            
            case Roll.Piece:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }


        //Show highlight on tile
        ShowLegalMove(legalMove);
        //Find only attack move (Move to enemy piece)
        CalculateAttackMove(legalMove, pFaction);
    }
    
    /// <summary>
    /// Calculate Legal move of pawn
    /// </summary>
    /// <param name="move">List of all possible movement of pawn</param>
    /// <param name="piece">Pawn it's self</param>
    /// <returns>List of legal move of pawn</returns>
    private static Vector2[] CurrentLegalMove(IEnumerable<Vector2> move, Piece piece)
    {
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;
        var temp = new List<Vector2>();
        
        foreach (var pos in move)
        {
            var tile = getTile(pos);
            
            //At this pos doesn't have tile on it
            if (!tile) continue;
            
            // in front of selected piece is not empty tile
            if (tile.occupiedPiece is not null && tile.GetPos().x - piece.pos.x == 0)
                break;
            
            // front straight tile is empty tile
            if (tile.occupiedPiece is null && tile.GetPos().x - piece.pos.x == 0)
                temp.Add(pos);
            
            // font left and right is not empty and occupiedPiece faction is not same 
            if (tile.occupiedPiece is not null && tile.occupiedPiece.faction != piece.faction && tile.GetPos().x - piece.pos.x != 0)
                temp.Add(pos);
            
        }

        return temp.ToArray();
    }
    
    /// <summary>
    /// Calculate legal with condition for piece that check occupiedPiece is alliance or not
    /// </summary>
    /// <param name="move">List of all possible movement of piece</param>
    /// <param name="faction">Piece team</param>
    /// <returns>List of legal move of piece</returns>
    private static Vector2[] CurrentLegalMove(IEnumerable<Vector2> move, Faction faction)
    {
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;
        var legalMove = new List<Vector2>();
        
        foreach (var pos in from pos in move
                 let tile = getTile(pos)
                 where tile && (tile.occupiedPiece is null || tile.occupiedPiece.faction != faction)
                 select pos)
        {
            LOG(pos);
            legalMove.Add(pos);
        }


        return legalMove.ToArray();
    }
    
    /// <summary>
    /// calculate legal move for piece that move multi-axis
    /// </summary>
    /// <param name="move">List of all possible movement of piece</param>
    /// <param name="faction">Piece team</param>
    /// <returns>List of legal move of piece</returns>
    private static Vector2[] CurrentLegalMove(IEnumerable<Vector2[]> move, Faction faction)
    {
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;
        var temp = new List<Vector2>();

        foreach (var axis in move)
        {
            foreach (var pos in axis)
            {
                var tile = getTile(pos);
                if (!tile) break;

                if (tile.occupiedPiece is not null)
                {
                    if (tile.occupiedPiece.faction == faction)
                        break;
                    temp.Add(pos);
                    break;
                }

                temp.Add(pos);
            }
        }
        return temp.ToArray();
    }

    /// <summary>
    /// Find that tile has enemy on it or not.
    /// </summary>
    /// <param name="legalMove">List of all possible movement of piece</param>
    /// <param name="faction">Piece team</param>
    private static void CalculateAttackMove(IEnumerable<Vector2> legalMove, Faction faction)
    {
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;

        List<Vector2> attackMove = new List<Vector2>();

        foreach (Vector2 pos in legalMove)
        {
            Tile tile = getTile(pos);
            
            if (!tile.occupiedPiece) continue;

            if (tile.occupiedPiece.faction == faction) continue;
            attackMove.Add(pos);

        }

        AttackMove = attackMove;
    }

    #endregion
    

    #region Piece move

    /// <summary>
    /// A bishop can move any number of squares diagonally, but cannot leap over other pieces.
    /// </summary>
    /// <param name="x">position on X-axis</param>
    /// <param name="y">position on Y-axis</param>
    /// <param name="faction">Piece Team</param>
    /// <returns>All possible movement of piece (included attack move)</returns>
    private static Vector2[] BishopWalk(float x, float y, Faction faction)
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

        var temp = new List<Vector2[]>
        {
            topLeftMove.ToArray(),
            downLeftMove.ToArray(),
            topRightMove.ToArray(),
            downRightMove.ToArray()
        };

        return CurrentLegalMove(temp.ToArray(), faction);
    }

    /// <summary>
    /// A rook can move any number of squares along a rank or file, but cannot leap over other pieces.
    /// Along with the king, a rook is involved during the king's castling move.
    /// </summary>
    /// <param name="x">position on X-axis</param>
    /// <param name="y">position on Y-axis</param>
    /// <param name="faction">Piece Team</param>
    /// <returns>All possible movement of piece (included attack move)</returns>
    private static Vector2[] RookWalk(float x, float y, Faction faction)
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

        var axisMove =  new List<Vector2[]>()
        {
            topMove.ToArray(),
            downMove.ToArray(),
            leftMove.ToArray(),
            rightMove.ToArray()
        };

        return CurrentLegalMove(axisMove, faction);
    }

    /// <summary>
    /// A knight moves to any of the closest squares that are not on the same rank, file, or diagonal.
    /// (Thus the move forms an "L"-shape: two squares vertically and one square horizontally,
    /// or two squares horizontally and one square vertically.)
    /// The knight is the only piece that can leap over other pieces.
    /// </summary>
    /// <param name="x">position on X-axis</param>
    /// <param name="y">position on Y-axis</param>
    /// <param name="faction">Piece Team</param>
    /// <returns>All possible movement of piece (included attack move)</returns>
    private static Vector2[] KnightWalk(float x, float y, Faction faction)
    {
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

        return CurrentLegalMove(temp.ToArray(), faction);
    }

    /// <summary>
    /// The king moves one square in any direction.
    /// There is also a special move called castling (Not finish yet) that involves moving the king and a rook.
    /// The king is the most valuable piece — attacks on the king must be immediately countered,
    /// and if this is impossible, immediate loss of the game ensues (see Check and checkmate below).
    /// </summary>
    /// <param name="x">position on X-axis</param>
    /// <param name="y">position on Y-axis</param>
    /// <param name="faction">Piece Team</param>
    /// <returns>All possible movement of piece (included attack move)</returns>
    private static Vector2[] KingWalk(float x, float y, Faction faction)
    {
        
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
        
        return CurrentLegalMove(temp.ToArray(), faction);
    }

    /// <summary>
    /// A pawn can move forward to the unoccupied square immediately in front of it on the same file,
    /// or on its first move it can advance two squares along the same file,
    /// provided both squares are unoccupied (black dots in the diagram).
    /// A pawn can capture an opponent's piece on a square diagonally in front of it by moving to that square (black crosses).
    /// It cannot capture a piece while advancing along the same file. A pawn has two special
    /// moves: the en passant capture and promotion.
    /// </summary>
    /// <param name="piece">Selected Piece</param>
    /// <returns>All possible movement of piece (included attack move)</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private static Vector2[] PawnWalk(Piece piece)
    {
        var move = new List<Vector2>();
        
        var piecePosX = piece.pos.x;
        var piecePosY = piece.pos.y;
        
        switch (piece.faction)
        {
            case Faction.WHITE when piece.isFirstMove:
                move.Add(new Vector2(piecePosX - 1, piecePosY + 1));
                move.Add(new Vector2(piecePosX + 1, piecePosY + 1));
                move.Add(new Vector2(piecePosX, piecePosY + 1));
                move.Add(new Vector2(piecePosX, piecePosY + 2));
                break;
            
            case Faction.WHITE:
                move.Add(new Vector2(piecePosX, piecePosY + 1));
                move.Add(new Vector2(piecePosX - 1, piecePosY + 1));
                move.Add(new Vector2(piecePosX + 1, piecePosY + 1));
                break;
            
            case Faction.BLACK when piece.isFirstMove:
                move.Add(new Vector2(piecePosX - 1, piecePosY - 1));
                move.Add(new Vector2(piecePosX + 1, piecePosY - 1));
                move.Add(new Vector2(piecePosX, piecePosY - 1));
                move.Add(new Vector2(piecePosX, piecePosY - 2));
                break;
            
            case Faction.BLACK:
                move.Add(new Vector2(piecePosX, piecePosY - 1));
                move.Add(new Vector2(piecePosX - 1, piecePosY - 1));
                move.Add(new Vector2(piecePosX + 1, piecePosY - 1));
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        return CurrentLegalMove(move.ToArray(), piece);
    }
    
    #endregion

    #region Show walkable tile
    
    /// <summary>
    /// Map highlight function to List of legal movement 
    /// </summary>
    /// <param name="legalMove">List of legal movement of current selected piece</param>
    private static void ShowLegalMove(IEnumerable<Vector2> legalMove)
    {
        var temp = new List<Vector2>();
        temp.AddRange(legalMove.Select(ShowHighlight));
        CurrentPieceMove = temp;

    }
    
    /// <summary>
    /// Show highlight on tile if this tile selected piece can move to it
    /// </summary>
    /// <param name="move">Position of move (Move to which tile)</param>
    /// <returns>It's self position</returns>
    private static Vector2 ShowHighlight(Vector2 move)
    {
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;
        
        Transform highlight = getTile(move).transform.GetChild(2);
                
        highlight.GameObject().SetActive(true);
        highlight.GetComponentInChildren<SpriteRenderer>().color = Color.green;

        return move;
    }
    
    #endregion

    public static bool IsCheck()
    {
        
        
        foreach (Vector2 move in AttackMove)
        {
            
        }

        return true;
    }
}
