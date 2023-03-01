using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPawnPromotionManager : PawnPromotionManager
{
    public static BlackPawnPromotionManager Instance;
    
    private void Awake()
    {
        Instance = this;
    }
}
