using UnityEngine;

public class Clickable : MonoBehaviour
{
    
    [SerializeField] private Tile self;
    
    private void OnMouseDown()
    {
        Debug.Log(name);
        
        Debug.Log(self.TileType());
    }
}
