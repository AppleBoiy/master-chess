using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pieces", menuName = "Scriptable Piece")]
public class ScriptablePiece : ScriptableObject
{
    public Faction faction;
    public Piece piecePrefab;

    
}

public enum Faction
{
    White = 0,
    Black = 1
}