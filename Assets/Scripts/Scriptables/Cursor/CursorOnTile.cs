
using System;
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
      
        
        
        if (turn == GameState.StartGame) return;
        var pieceOnTile = _tileOnPos.occupiedPiece;
        
        if (!pieceOnTile)
        {
            cursorManager.OnEmpty();
            return;
        }

        if (Piece.AttackMove != null)
        {
            bool CanAttack(Vector2 pos) => (Vector2)transform.position == pos;

            if (Piece.AttackMove.Any(CanAttack) && CorrectPlayerTurn(pieceOnTile))
            {
                Debug.Log(true);
                cursorManager.Attack();
                return;
            }
        }

        switch (pieceOnTile.faction)
        {
            case Faction.BLACK when turn == GameState.BlackTurn:
                cursorManager.OnAlliance();
                break;

            case Faction.WHITE when turn == GameState.WhiteTurn:
                cursorManager.OnAlliance();
                break;
            
            default:
                cursorManager.ResetCursor();
                break;
        }
    }

    //Piece that prepare to attack has Faction as same as Player turn
    //etc. if this turn is white turn only white piece can attack to black piece
    //     otherwise black piece only can attack to white piece
    private static bool CorrectPlayerTurn(Piece pieceOnTile)
    {
        GameState state = GameManager.Instance.State;

        return state switch
        {
            GameState.BlackTurn or GameState.CheckWhite when pieceOnTile.faction == Faction.WHITE => true,
            GameState.WhiteTurn or GameState.CheckBlack when pieceOnTile.faction == Faction.BLACK => true,
            _ => false
        };
    }
} 