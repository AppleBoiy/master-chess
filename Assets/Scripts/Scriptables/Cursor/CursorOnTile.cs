
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
            if (Piece.AttackMove.Any(pos => (Vector2)transform.position == pos))
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
                cursorManager.OnEnemy();
                break;
        }
    }
} 