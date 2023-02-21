
using System;

public class WhiteTeam : WhitePieces
{
    public static WhiteTeam Instance;

    private void Awake()
    {
        Instance = this;
    }
}

