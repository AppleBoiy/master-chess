using UnityEngine;

public interface IPieceStartingPos
{  
    
    public interface  IWhite: IPieceStartingPos
    {
        public static readonly Vector2 FirstPawn = new(1, 0);
        public static readonly Vector2 LastPawn = new(1, 8);
        public static readonly Vector2 WhiteKing = new(0, 0);
    }
    
    public interface IBlack : IPieceStartingPos
    {
        public static readonly Vector2 FirstPawn = new(6, 0);
        public static readonly Vector2 LastPawn = new(6, 8);
        public static readonly Vector2 BlackKing = new(7, 0);
    }
}