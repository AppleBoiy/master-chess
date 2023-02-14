using UnityEngine;

public class Clickable : MonoBehaviour
{
    
    [SerializeField] private Tile self;
    
    
    
    private void OnMouseDown()
    {
        Debug.Log(self.TileType() == "HOLE"
            ? $"<color=red>{self.TileType()}</color> at pos <color=yellow>{self.GetPos()}</color>"
            : $"<color=green>{self.TileType()}</color> at pos <color=yellow>{self.GetPos()}</color>");
    }
    
    
}
