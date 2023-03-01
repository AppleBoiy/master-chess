using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePawnPromotionManager : PawnPromotionManager
{
    public static WhitePawnPromotionManager Instance;
    
    private void Awake()
    {
        Instance = this;
    }
}
