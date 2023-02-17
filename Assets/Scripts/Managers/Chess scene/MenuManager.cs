using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    
    #region params


    [Space(3)] 
    [Header("Panel")] 
    [SerializeField] private GameObject selectedPiece;
    [SerializeField] private GameObject tileInfo;
    [SerializeField] private GameObject pieceOnTile;
    
    public static MenuManager Instance;
    
    #endregion
    
    private void Awake()
    {
        Instance = this;
    }
    

    public void ShowSelectedPiece(Piece piece)
    {
        Debug.Log("Show piece is selected");
        
        if (piece == null)
        {
            Debug.Log("Don't have any piece on this tile");
            
            selectedPiece.SetActive(false);
            return;
        }

        Debug.Log("Something on this tile..");
        
        selectedPiece.GetComponentInChildren<TMP_Text>().text = piece.Roll ;
        selectedPiece.SetActive(true);
    }

    public void ShowTileInfo(Tile tile)
    {
        
        if (tile == null)
        {
            tileInfo.SetActive(false);
            pieceOnTile.SetActive(false);
            return;
        }
        
        tileInfo.GetComponentInChildren<TMP_Text>().text = tile.name;
        tileInfo.SetActive(true);

        if (!tile.OccupiedPiece) return;
        pieceOnTile.GetComponentInChildren<TMP_Text>().text = tile.OccupiedPiece.Roll;
        pieceOnTile.SetActive(true);
    }
}
