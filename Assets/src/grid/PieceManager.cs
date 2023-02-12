using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceManager : MonoBehaviour
{

    #region params

    public static PieceManager Instance;

    private List<ScriptablePiece> _pieces;
    
    #endregion

    private void Awake()
    {
        Instance = this;

        //found But not found WHAT!!!
        _pieces = Resources.LoadAll<ScriptablePiece>("GridScene").ToList();

        foreach (var piece in _pieces)
        {
            Debug.Log(piece);
        }
    }

}
