using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManger : Manager<UIManger>
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UserWin.AddListener(UserWinUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UserWinUI(){
        Debug.Log("UI:User win");
    }
}
