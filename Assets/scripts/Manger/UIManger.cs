using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManger : Manager<UIManger>
{
    public GameObject Result;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UserWin.AddListener(UserWinUI);
        Result.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UserWinUI(){
        Debug.Log("UI:User win");
        Result.SetActive(true);
    }

    public void Init(){
         Result.SetActive(false);
         GetComponentInChildren<BlockPanel>().Reset();
    }
}
