using UnityEngine;

public interface IPieceStartingPos
{

    public interface  IWhite: IPieceStartingPos
    {
        
        
        public static readonly Vector2 FirstPawn = new(1, 0);
        public static readonly Vector2 LastPawn = new(1, 8);
        
        public static readonly Vector2 Rook1 = new(0, 0);
        public static readonly Vector2 Rook2 = new(0, 7);

        public static readonly Vector2 Knight1 = new(0, 1);
        public static readonly Vector2 Knight2 = new(0, 6);

        public static readonly Vector2 Bishop1 = new(0, 2);
        public static readonly Vector2 Bishop2 = new(0, 5);
        
        public static readonly Vector2 King = new(0, 3);
        public static readonly Vector2 Queen = new(0, 4);

    }
    
    public interface IBlack : IPieceStartingPos
    {
        public static readonly Vector2 FirstPawn = new(6, 0);
        public static readonly Vector2 LastPawn = new(6, 8);
        
        public static readonly Vector2 Rook1 = new(7, 0);
        public static readonly Vector2 Rook2 = new(7, 7);

        public static readonly Vector2 Knight1 = new(7, 1);
        public static readonly Vector2 Knight2 = new(7, 6);

        public static readonly Vector2 Bishop1 = new(7, 2);
        public static readonly Vector2 Bishop2 = new(7, 5);
        
        public static readonly Vector2 King = new(7, 3);
        public static readonly Vector2 Queen = new(7, 4);
    }
}