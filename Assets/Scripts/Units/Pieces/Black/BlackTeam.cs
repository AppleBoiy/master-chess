

using System;

public class BlackTeam : BlackPieces
{
    public static BlackTeam Instance;

    private void Awake()
    {
        Instance = this;
    }
}
