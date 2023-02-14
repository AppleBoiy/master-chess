using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class MenuManager : MonoBehaviour
{

    #region params

    [Header("Menu Manager")] 
    [SerializeField] private GameObject colorSelectPanel;
    [SerializeField] public Button testButton;

    [Space(3)] 
    [Header("Panel")] 
    [SerializeField] private GameObject selectedPiece, tileInfo, pieceOnTile;
    
    
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
