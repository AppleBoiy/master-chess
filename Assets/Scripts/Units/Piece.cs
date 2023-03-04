using System;
using System.Collections.Generic;
using System.Linq;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using static GameState;
using static Roll;

public abstract class Piece : MonoBehaviour
{
    #region params
    
    [FormerlySerializedAs("OccupiedTile")] 
    public Tile occupiedTile;
    
    [FormerlySerializedAs("Faction")] 
    public Faction faction;

    [FormerlySerializedAs("Roll")] 
    public Roll roll;
    public bool isFirstMove;
    
    public static List<Vector2> CurrentPieceMove;
    public static List<Vector2> AttackMove;

    public Vector2 pos;
    
    #endregion
    
    #region Calculate Move

    /// <summary>
    /// CalculateLegalMove() is a function that calculates legal move for a piece
    /// </summary>
    /// <param name="piece">The piece that we want to calculate the legal move for.</param>
    public static Vector2[] CalculateLegalMove(Piece piece)
    {
        float piecePosX =  piece.pos.x;
        float piecePosY =  piece.pos.y;
        Faction pFaction = piece.faction;
        Vector2[] legalMove;

         /* Checking the roll of the piece and then calling the appropriate function to get the legal
             moves. */
        switch (piece.roll)
        {
            case Queen:
                //Queen move is  Rook and Bishop Legal move (Both of them)
                Vector2[] bishopWalk = BishopWalk(piecePosX, piecePosY, pFaction);
                Vector2[] rookWalk = RookWalk(piecePosX, piecePosY, pFaction);
                legalMove = bishopWalk.Union(rookWalk).ToArray();
                break;
            
            case King:
                legalMove = KingWalk(piecePosX, piecePosY, pFaction);
                break;

            case Knight:
                legalMove = KnightWalk(piecePosX, piecePosY, pFaction);
                break;
            
            case Rook:
                legalMove = RookWalk(piecePosX, piecePosY, pFaction);
                break;
            
            case Bishop:
                legalMove = BishopWalk(piecePosX, piecePosY, pFaction);
                break;
            
            case Pawn:
                legalMove = PawnWalk(piece);
                break;
            
            case Roll.Piece:
                
                //Dummy for tester
                legalMove = TileManager.Instance.DictTiles?.Keys.ToArray();
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        return legalMove;
    }

    public static void ShowNormalMove(Piece piece)
    {
        Vector2[] legalMove = CalculateLegalMove(piece);
        
        //Show highlight on tile
        ShowLegalMove(legalMove);
        
        //Find only attack move (Move to enemy piece)
        CalculateAttackMove(legalMove, piece.faction);
    }
    
    public static void CalculateCheckKing(Piece piece, Faction attackTeam)
    {
        Vector2 enemyKingPos = (attackTeam is Faction.Black) ? WhiteTeam.KingPos : BlackTeam.KingPos; 
        foreach (Vector2 move in  CalculateLegalMove(piece))
        {
            if (move != enemyKingPos) continue;
            ShowCheck(move);
            return;
        }
    }
   
    /// <summary>
    /// > This function is used to get the current legal move of the selected piece
    /// </summary>
    /// <param name="move">The list of possible moves for the piece.</param>
    /// <param name="piece">The piece that is currently selected.</param>
    /// <returns>
    /// an array of Vector2.
    /// </returns>
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
            if (tile.occupiedPiece != null && tile.GetPos().x - piece.pos.x == 0)
                break;
            
            // front straight tile is empty tile
            if (tile.occupiedPiece == null && tile.GetPos().x - piece.pos.x == 0)
            {
                temp.Add(pos);
                continue;
            }

            // font left and right is not empty and occupiedPiece faction is not same 
            if (tile.occupiedPiece != null && tile.occupiedPiece.faction != piece.faction && tile.GetPos().x - piece.pos.x != 0)
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
        return (
            from pos in move 
            let tile = getTile(pos) 
            where tile && (tile.occupiedPiece is null || tile.occupiedPiece.faction != faction) 
            select pos
        ).ToArray();
    }
    
   
    /// <summary>
    /// > This function takes in a list of possible moves and returns a list of legal moves
    /// </summary>
    /// <param name="move">The list of vectors that the piece can move to.</param>
    /// <param name="faction">The faction of the piece that is moving.</param>
    /// <returns>
    /// A Vector2 array of the current legal moves for the piece.
    /// </returns>
    private static Vector2[] CurrentLegalMove(IEnumerable<Vector2[]> move, Faction faction)
    {
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;
        var temp = new List<Vector2>();

        /* Checking if the tile is occupied by a piece of the same faction. If it is, it breaks out of
        the loop. If it is not, it adds the position to the list. */
        foreach (var axis in move)
        {
            foreach (var pos in axis)
            {
                var tile = getTile(pos);
                if (!tile) break;

                if (tile.occupiedPiece != null)
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
    /// If the tile is occupied by a piece, and the piece is not on the same faction as the piece that
    /// is moving, then add the tile to the list of attack moves
    /// </summary>
    /// <param name="legalMove">The list of legal moves that the piece can make.</param>
    /// <param name="faction">The faction of the piece that is currently selected.</param>
    private static void CalculateAttackMove(IEnumerable<Vector2> legalMove, Faction faction)
    {
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;
        
        List<Vector2> attackMove = new List<Vector2>();

        /* Checking if the tile is occupied by a piece of the same faction. If it is, it will continue.
        If it is not, it will add it to the attackMove list. */
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
    /// It takes in the current position of the bishop and the faction of the bishop and returns an
    /// array of all the possible legal moves the bishop can make
    /// </summary>
    /// <param name="x">The x position of the piece</param>
    /// <param name="y">The y position of the piece</param>
    /// <param name="faction">The faction of the piece.</param>
    /// <returns>
    /// The legal moves for the bishop.
    /// </returns>
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
    /// It takes in the current position of the rook, and returns an array of all the possible moves the
    /// rook can make
    /// </summary>
    /// <param name="x">The x position of the piece</param>
    /// <param name="y">The y position of the piece</param>
    /// <param name="faction">The faction of the piece.</param>
    /// <returns>
    /// The legal moves for the rook.
    /// </returns>
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
        
        /* Checking the position of the piece and adding the possible moves to the list. */
        switch (piece.faction, piece.isFirstMove)
        {
            case (Faction.White, true):
                move.Add(new Vector2(piecePosX - 1, piecePosY + 1));
                move.Add(new Vector2(piecePosX + 1, piecePosY + 1));
                move.Add(new Vector2(piecePosX, piecePosY + 1));
                move.Add(new Vector2(piecePosX, piecePosY + 2));
                break;

            case (Faction.White, _):
                move.Add(new Vector2(piecePosX - 1, piecePosY + 1));
                move.Add(new Vector2(piecePosX + 1, piecePosY + 1));
                move.Add(new Vector2(piecePosX, piecePosY + 1));
                break;

            case (Faction.Black, true):
                move.Add(new Vector2(piecePosX - 1, piecePosY - 1));
                move.Add(new Vector2(piecePosX + 1, piecePosY - 1));
                move.Add(new Vector2(piecePosX, piecePosY - 1));
                move.Add(new Vector2(piecePosX, piecePosY - 2));
                break;
            
            case (Faction.Black, _):
                move.Add(new Vector2(piecePosX - 1, piecePosY - 1));
                move.Add(new Vector2(piecePosX + 1, piecePosY - 1));
                move.Add(new Vector2(piecePosX, piecePosY - 1));
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
        CurrentPieceMove = new List<Vector2>(legalMove.Select(ShowHighlight));
    }

  
    /// <summary>
    /// > ShowHighlight takes a Vector2 and returns a Vector2
    /// </summary>
    /// <param name="move">The position of the tile you want to highlight.</param>
    /// <returns>
    /// The move that was passed in.
    /// </returns>
    private static Vector2 ShowHighlight(Vector2 move)
    {
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;
        
        Transform highlight = getTile(move).transform.GetChild(2);
        
        highlight.GameObject().SetActive(true);
        highlight.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        
        return move;
    }
    
    private static void ShowCheck(Vector2 move)
    {
        Func<Vector2,Tile> getTile = TileManager.Instance.GetTile;
        
        Transform highlight = getTile(move).transform.GetChild(2);
        
        highlight.GameObject().SetActive(true);
        highlight.GetComponentInChildren<SpriteRenderer>().color = Color.magenta;
    }
    #endregion

    
    /// <summary>
    /// If the pawn is at the end of the board, it is promoted
    /// </summary>
    public abstract void CheckPawnPromotion();

    
    /// <summary>
    /// PromotionPawn is called when a pawn reaches the other side of the board and is promoted to a new
    /// piece
    /// </summary>
    /// <param name="promotionToPiece">The piece that is being promoted.</param>
    public abstract void PromotionPawn(Piece promotionToPiece);

    private void OnDestroy()
    {
        if (roll is Roll.Piece) return;
        if (GameManager.Instance?.state is StartGame or Promotion) return;
        
        PieceSfx.Instance.DestroyPieceSfx();
        
    }
}
