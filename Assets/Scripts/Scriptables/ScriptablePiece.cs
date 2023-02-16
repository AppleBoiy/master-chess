using UnityEngine;

[CreateAssetMenu(fileName = "New Pieces", menuName = "Scriptable Piece")]
public class ScriptablePiece : ScriptableObject
{
    public Faction Faction;
    public Piece piecePrefab;

    
}
