
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Piece : MonoBehaviour
{
    #region params
    
    [FormerlySerializedAs("OccupiedTile")] 
    public Tile occupiedTile;
    
    [FormerlySerializedAs("Faction")] 
    public Faction faction;
    
    [FormerlySerializedAs("Roll")] 
    public Roll roll;
    
    public Vector2 pos;
    

    #endregion

    public static List<Piece> CalPiecesLeft(GameObject pieceHolder)
    {
        List<Piece> pieces = new();
        
        for (var i = 0; i < pieceHolder.transform.childCount; i++)
        {
            Piece piece = pieceHolder.transform.GetChild(i).GetComponentInChildren<Piece>();
            
            Debug.Log($"pieces {piece}");
            
            pieces.Add(piece);

        }

        return pieces;
    }

}
