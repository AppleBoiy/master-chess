

using System;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTeam : MonoBehaviour, IPiecesInGame
{
    public static WhiteTeam Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Piece> FindAllAlliance()
    {
        List<Piece> alliance = new List<Piece>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log(transform.GetChild(i));
        }

        return alliance;
    }
}
