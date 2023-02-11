
using UnityEngine;

public class GridSceneManager : GameManager
{
    #region params

    [Header("Initialize field")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private PiecesHandler piecesHandler;

    #endregion
}
