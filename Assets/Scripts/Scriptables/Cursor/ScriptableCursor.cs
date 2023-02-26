using UnityEngine;

public class ScriptableCursor : MonoBehaviour
{
    
    private void OnMouseExit()
    {
        CursorManager.Instance.ResetCursor();
    }
    
}
