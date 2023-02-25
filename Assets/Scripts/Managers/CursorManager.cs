using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Cursor;

public class CursorManager : MonoBehaviour
{

    #region params
    [SerializeField] private Texture2D Default, attack, onAlliance, onEnemy, onEmpty;
    
    public static CursorManager Instance;    

    
    #endregion
    

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeCursor()
    {
        SetCursor(attack, new Vector2(20, 20), CursorMode.Auto);
    }

    public void ResetCursor()
    {
        SetCursor(Default, new Vector2(20, 20), CursorMode.Auto);
    }
}
