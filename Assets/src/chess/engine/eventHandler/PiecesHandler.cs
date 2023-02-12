using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PiecesHandler : MonoBehaviour
{

    #region params

    [SerializeField] private List<Pieces> pieces;
    [SerializeField] private Player whitePlayer;
    [SerializeField] private Player blackPlayer;
    
    public static PiecesHandler Instance;
    
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void SelectColor(string color)
    {
        GameManager.Instance.UpdateGameState(GameState.WhitePlayerTurn);
    }

    public void CurrentPlayer(GameState gameState)
    {
        Debug.Log(gameState);
    }
    
}

enum PlayerTurn
{
    WHITE,
    BLACK
}