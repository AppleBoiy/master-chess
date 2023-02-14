using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Pieces", menuName = "Scriptable Piece")]
public class ScriptablePiece : ScriptableObject
{
    [FormerlySerializedAs("faction")] public GridSceneFaction gridSceneFaction;
    public Piece piecePrefab;

    
}

public enum GridSceneFaction
{
    White = 0,
    Black = 1
}