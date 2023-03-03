using System.Linq;
using UnityEngine;
using static Faction;
using static GameState;

public class CursorOnTile : ScriptableCursor
{
    private Tile _tileOnPos;
    
    /// <summary>
    /// It gets the tile that the player is currently on.
    /// </summary>
    private void SetTile()
    {
        _tileOnPos = TileManager.Instance.GetTile(transform.position);
    }

    
    /// <summary>
    /// > If the tile is empty, set the cursor to the empty cursor. If the tile has a piece, check if
    /// the piece is an enemy or an ally. If the piece is an ally, set the cursor to the ally cursor. If
    /// the piece is an enemy, set the cursor to the enemy cursor. If the piece is an enemy and the
    /// player is in attack mode, set the cursor to the attack cursor
    /// </summary>
    /// <returns>
    /// The return type is void.
    /// </returns>
    private void OnMouseEnter()
    {
        SetTile();
        
        CursorManager cursorManager = CursorManager.Instance;
        GameState turn = GameManager.Instance.state;

        /* It checks if the game is in the start game scene or the promotion scene. If the game is in
        the start game scene or the promotion scene, the player cannot interact with the board. */
        if (turn is  StartGame or Promotion) return;
        
        var pieceOnTile = _tileOnPos.occupiedPiece;
        
        //Empty tile
        if (!pieceOnTile)
        {
            cursorManager.OnEmpty();
            return;
        }

        //Attack phase
        /* It checks if the piece is in attack mode and if the piece can attack the tile that the
        player is currently on. */
        if (Piece.AttackMove != null)
        {
            bool CanAttack(Vector2 pos) => (Vector2)transform.position == pos;

            if (Piece.AttackMove.Any(CanAttack) && CorrectPlayerTurn(pieceOnTile))
            {
                cursorManager.Attack();
                return;
            }
        }

        /* It checks if the piece is an ally or an enemy. If the piece is an ally, set the cursor to
        the ally cursor. If the piece is an enemy, set the cursor to the enemy cursor. */
        switch (pieceOnTile.faction, turn)
        {
            case (Black, BlackTurn):
                cursorManager.OnAlliance();
                break;

            case (White, WhiteTurn):
                cursorManager.OnAlliance();
                break;
            
            default:
                cursorManager.OnEnemy();
                break;
        }
    }

    /// <summary>
    /// If the game state is either BlackTurn or CheckWhite and the piece on the tile is white, or if
    /// the game state is either WhiteTurn or CheckBlack and the piece on the tile is black, then return
    /// true. Otherwise, return false
    /// </summary>
    /// <param name="pieceOnTile">The piece that is being moved.</param>
    /// <returns>
    /// A boolean value.
    /// </returns>
    private static bool CorrectPlayerTurn(Piece pieceOnTile)
    {
        return GameManager.Instance.state switch
        {
            BlackTurn or CheckWhite when pieceOnTile.faction is White => true,
            WhiteTurn or CheckBlack when pieceOnTile.faction is Black => true,
            _ => false
        };
    }
} 