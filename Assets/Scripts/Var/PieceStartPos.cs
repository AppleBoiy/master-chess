using UnityEngine;

public interface  IWhite
{
        
        
    public static readonly Vector2 FirstPawn = new(0, 1);
    public static readonly Vector2 LastPawn = new(7, 1);
        
    public static readonly Vector2 Rook1 = new(0, 0);
    public static readonly Vector2 Rook2 = new(7, 0);

    public static readonly Vector2 Knight1 = new(1, 0);
    public static readonly Vector2 Knight2 = new(6, 0);

    public static readonly Vector2 Bishop1 = new(2, 0);
    public static readonly Vector2 Bishop2 = new(5, 0);
        
    public static readonly Vector2 King = new(3, 0);
    public static readonly Vector2 Queen = new(4, 0);

}
    
public interface IBlack 
{
    public static readonly Vector2 FirstPawn = new(0, 6);
    public static readonly Vector2 LastPawn = new(7, 6);
        
    public static readonly Vector2 Rook1 = new(0, 7);
    public static readonly Vector2 Rook2 = new(7, 7);

    public static readonly Vector2 Knight1 = new(1, 7);
    public static readonly Vector2 Knight2 = new(6, 7);

    public static readonly Vector2 Bishop1 = new(2, 7);
    public static readonly Vector2 Bishop2 = new(5, 7);
        
    public static readonly Vector2 King = new(3, 7);
    public static readonly Vector2 Queen = new(4, 7);
}