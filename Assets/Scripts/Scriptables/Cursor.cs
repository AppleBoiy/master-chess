using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    
    private void OnMouseExit()
    {
        CursorManager.Instance.ResetCursor();
    }

    private void OnMouseEnter()
    {
        
        CursorManager.Instance.ChangeCursor();
    }
}
