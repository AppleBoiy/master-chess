using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Piece : MonoBehaviour
{
    #region params
    
    public Tile OccupiedTile;
    
    [FormerlySerializedAs("Faction")] public GridSceneFaction gridSceneFaction;
    public Vector2 pos;
    public string Roll;

    #endregion


}
