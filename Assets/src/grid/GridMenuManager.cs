using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GridMenuManager : MenuManager
{
    public new static GridMenuManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public async void ChangeTurn()
    {
        Debug.Log ("Change turn!! in GrindMenuManager");
        
        testButton.interactable = false;

        await Task.Delay(2000);
        
        GridSceneManager.Instance.ChangeTurn();

        testButton.interactable = true;
    }
}
