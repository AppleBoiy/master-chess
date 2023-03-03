using UnityEngine;
using UnityEngine.Serialization;
using static UnityEngine.Cursor;


public class CursorManager : MonoBehaviour
{

    #region params
    [FormerlySerializedAs("Default")] 
    [SerializeField] private Texture2D @default;
    [SerializeField] private Texture2D attack, onAlliance, onEnemy, onEmpty;

    private static readonly Vector2 HotspotCursor = new(20, 20);
    public static CursorManager Instance;
    

    #endregion
    

    private void Awake()
    {
        Instance = this;
    }

    
    /// <summary>
    /// > SetCursor(Texture2D texture, Vector2 hotspot, CursorMode cursorMode)
    /// 
    /// The first parameter is the texture you want to use as the cursor. The second parameter is the
    /// hotspot of the cursor. The third parameter is the cursor mode
    /// </summary>
    #region Set in game player cursor

    public void ResetCursor()
    {
        SetCursor(@default, HotspotCursor, CursorMode.Auto);
    }

    public void Attack()
    {
        SetCursor(attack, HotspotCursor, CursorMode.Auto);
    }

    public void OnAlliance()
    {
        SetCursor(onAlliance, HotspotCursor, CursorMode.Auto);
    }

    public void OnEnemy()
    {
        SetCursor(onEnemy, HotspotCursor, CursorMode.Auto);
    }

    public void OnEmpty()
    {
        SetCursor(onEmpty, HotspotCursor, CursorMode.Auto);
    }

    #endregion
    
}
