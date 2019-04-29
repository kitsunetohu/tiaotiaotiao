using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour//ブロックというクラスの特徴はユーザーに放置されることができる
{
    
    //经过后是否消失
    public bool canThrough;
    public Material undown;
    public Material down;
    public Material warning;
    public LayerMask cantOverlap;

    bool isPickUp;
    bool overlapWarning=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPickUp&&overlapWarning){
            
        }
    }

    void OnTriggerStay(Collider collider){
        if(collider.gameObject.tag=="Player"||collider.gameObject.layer==cantOverlap){
            overlapWarning=true;
        }
    }

    public void PutItUp(){
        GetComponent<MeshRenderer> ().material = undown;
        GetComponent<Collider>().isTrigger=false;
        isPickUp=true;
    }
    
    public void PutItDown(){
        isPickUp=false;
        GetComponent<MeshRenderer> ().material = down;
        GetComponent<Collider>().isTrigger=true;
    }
}
