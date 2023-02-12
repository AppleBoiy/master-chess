using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GridMenuManager : MenuManager
{
    
    [SerializeField] private GridSceneManager gridManager;

    public static GridMenuManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public new async void ChangeTurn()
    {
        Debug.Log ("Change turn!! in GrindMenuManager");
        
        testButton.interactable = false;

        await Task.Delay(2000);
        
        GridSceneManager.Instance.ChangeTurn();

        testButton.interactable = true;
    }
}
