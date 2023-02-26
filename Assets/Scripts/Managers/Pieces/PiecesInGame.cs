using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPiecesInGame
{
    public List<Piece> GetAllAlliance(Faction faction)
    {
        return faction == Faction.BLACK 
            ? BlackTeam.Instance.FindAllAlliance() 
            : WhiteTeam.Instance.FindAllAlliance();
    }
}

