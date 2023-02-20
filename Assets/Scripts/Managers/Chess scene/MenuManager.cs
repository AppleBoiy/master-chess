using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    
    #region params

    [Header("Start Scene")] 
    [SerializeField] private GameObject startGameHolder;
    

    [Space(3)] 
    [Header("Panel")] 
    [SerializeField] private GameObject selectedPiece;
    [SerializeField] private GameObject tileInfo;
    [SerializeField] private GameObject pieceOnTile;
    [SerializeField] private GameObject playerTurn;
    
    public static MenuManager Instance;
    private TMP_Text _turnDialog;
    
    #endregion

    private void Start()
    {
        _turnDialog = playerTurn.GetComponentInChildren<TMP_Text>();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        
        _turnDialog.text = GameManager.Instance.State.ToString();
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
        
        selectedPiece.GetComponentInChildren<TMP_Text>().text = piece.roll.ToString() ;
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
        pieceOnTile.GetComponentInChildren<TMP_Text>().text = tile.OccupiedPiece.roll.ToString();
        pieceOnTile.SetActive(true);
    }


    public void SelectWhitePlayer()
    {
        GameManager.Instance.UpdateGameState(GameState.WhiteTurn);
        startGameHolder.SetActive(false);
    }

    public void SelectBlackPlayer()
    {
        GameManager.Instance.UpdateGameState(GameState.BlackTurn);
        startGameHolder.SetActive(false);
    }
}
