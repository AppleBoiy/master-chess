using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region params

    [Header("Initialize field")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private PiecesHandler piecesHandler;

    #endregion

}
