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
    public GameObject debugBall;


    bool isPickUp = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected void Update()
    {
        
        if (isPickUp)
        {
           
            if (OverlapWarning())
            {
                GetComponent<MeshRenderer>().material = warning;
            }
            else
            {
                GetComponent<MeshRenderer>().material = undown;
            }

        }
    }

    bool OverlapWarning()
    {
        Collider[] collisions = Physics.OverlapBox(transform.position, GetComponent<BoxCollider>().size / 2.0f, transform.rotation);
        
       
        foreach (Collider x in collisions)
        {   
            if (x.gameObject.CompareTag("Player") || x.gameObject.CompareTag("cantOverlap"))
            {
                if(x.gameObject!=gameObject)return true;
               
            }
        }
        return false;
    }
    void OnDrawGizmosSelected(){
         Gizmos.color = new Color(1, 1, 0, 1.0f);
         Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().size);
    }

    public void PutItUp()
    {
        GetComponent<MeshRenderer>().material = undown;
        GetComponent<Collider>().isTrigger = true;
        isPickUp = true;
       
    }

    public bool PutItDown()
    {
        if (OverlapWarning() == true)
        {
            return false;
        }
        isPickUp = false;
        GetComponent<MeshRenderer>().material = down;
        GetComponent<Collider>().isTrigger = false;

        return true;
    }
}
