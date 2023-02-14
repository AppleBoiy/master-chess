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
    [SerializeField] private GameObject selectedPiece;
    
    public static MenuManager Instance;

    #endregion


    private void Awake()
    {
        Instance = this;
    }


    public void ShowSelectedPiece(Piece piece)
    {
        if (piece == null)
        {
            selectedPiece.SetActive(false);
            return;
        }

        selectedPiece.GetComponentInChildren<TMP_Text>().text = piece.Roll ;
        selectedPiece.SetActive(true);
    }


    
}
