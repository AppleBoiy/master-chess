using System;
using UnityEngine;
using UnityEngine.UI;

public class OnClickBtn : MonoBehaviour {
    public Button yourButton;

    void Start () {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick(){
        Debug.Log ("Change turn!!");
        
    }

}