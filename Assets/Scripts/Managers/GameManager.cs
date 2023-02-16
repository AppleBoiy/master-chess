using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region params

    public static GameManager Instance;

    #endregion

    private void Awake()
    {
        Instance = this;
    }
}
