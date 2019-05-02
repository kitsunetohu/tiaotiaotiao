using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            //win
            GameManager.Instance.getGameResult(true);
            
        }
        Debug.Log("win");
    }
}
