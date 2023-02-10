using UnityEngine;

public class Clickable : MonoBehaviour
{
    
    [SerializeField] private Tile self;
    
    
    
    private void OnMouseDown()
    {
        if (self.TileType() == "HOLE") 
            Debug.Log($"<color=red>{self.TileType()}</color> at pos <color=yellow>{self.GetPos()}</color>");
        else
            Debug.Log($"<color=green>{self.TileType()}</color> at pos <color=yellow>{self.GetPos()}</color>");
    }
    
    
}
