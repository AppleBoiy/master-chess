using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    #region params

    public static MenuManager Instance;

    #endregion

    private void Awake()
    {
        Instance = this;
    }
}