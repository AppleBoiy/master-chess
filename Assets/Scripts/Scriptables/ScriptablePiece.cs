using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Pieces", menuName = "Scriptable Piece")]
public class ScriptablePiece : ScriptableObject
{
    [FormerlySerializedAs("Faction")] 
    public Faction faction;
    public Piece piecePrefab;

    
}
