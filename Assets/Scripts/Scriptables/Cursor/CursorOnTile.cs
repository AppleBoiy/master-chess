using System.Linq;
using UnityEngine;

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
        if (turn is GameState.StartGame) return;
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
        switch (pieceOnTile.faction)
        {
            case Faction.BLACK when turn is GameState.BlackTurn:
            case Faction.WHITE when turn is GameState.WhiteTurn:
                cursorManager.OnAlliance();
                break;
            
            default:
                cursorManager.OnEnemy();
                break;
        }
    }

    //Piece that prepare to attack has Faction as same as Player turn
    //etc. if this turn is white turn only white piece can attack to black piece
    //     otherwise black piece only can attack to white piece
    private static bool CorrectPlayerTurn(Piece pieceOnTile)
    {
        return GameManager.Instance.State switch
        {
            //Black turn or Black is check white king 
            GameState.BlackTurn or GameState.CheckWhite
                when pieceOnTile.faction is Faction.WHITE => true,

            //White turn or White is check black king
            GameState.WhiteTurn or GameState.CheckBlack
                when pieceOnTile.faction is Faction.BLACK => true,

            //Otherwise
            _ => false
        };
    }
} 