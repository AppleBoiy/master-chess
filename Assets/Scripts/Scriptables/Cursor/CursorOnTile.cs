using System.Linq;
using UnityEngine;
using static Faction;
using static GameState;

public  class CursorOnTile : ScriptableCursor
{
    private Tile _tileOnPos;
    
    private void SetTile()
    {
        _tileOnPos = TileManager.Instance.GetTile(transform.position);
    }

    
    private void OnMouseEnter()
    {
        SetTile();
        
        CursorManager cursorManager = CursorManager.Instance;
        GameState turn = GameManager.Instance.State;
        
        //On start game scene do not check piece on tile to avoid error
        //Promotion scene player can't interaction with board
        if (turn is  StartGame or Promotion) return;
        
        var pieceOnTile = _tileOnPos.occupiedPiece;
        
        //Empty tile
        if (!pieceOnTile)
        {
            cursorManager.OnEmpty();
            return;
        }

        //Attack phase
        if (Piece.AttackMove != null)
        {
            bool CanAttack(Vector2 pos) => (Vector2)transform.position == pos;

            if (Piece.AttackMove.Any(CanAttack) && CorrectPlayerTurn(pieceOnTile))
            {
                cursorManager.Attack();
                return;
            }
        }

        //Selected piece phase
        switch (pieceOnTile.faction, turn)
        {
            case (BLACK, BlackTurn):
                cursorManager.OnAlliance();
                break;

            case (WHITE, WhiteTurn):
                cursorManager.OnAlliance();
                break;
            
            default:
                cursorManager.OnEnemy();
                break;
        }
    }


    /// <summary>
    ///Piece that prepare to attack has Faction as same as Player turn
    ///etc. if this turn is white turn only white piece can attack to black piece
    ///     otherwise black piece only can attack to white piece
    /// </summary>
    /// <param name="pieceOnTile"></param>
    /// <returns></returns>
    private static bool CorrectPlayerTurn(Piece pieceOnTile)
    {
        return GameManager.Instance.State switch
        {
            BlackTurn or CheckWhite when pieceOnTile.faction is WHITE => true,
            WhiteTurn or CheckBlack when pieceOnTile.faction is BLACK => true,
            _ => false
        };
    }
} 