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


    bool isPickUp = false;
    bool overlapWarning = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected void Update()
    {
        Debug.Log("isPickUp");
        if (isPickUp)
        {

            if (overlapWarning)
            {
                GetComponent<MeshRenderer>().material = warning;
            }
            else
            {
                GetComponent<MeshRenderer>().material = undown;
            }

        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player" || (collider.gameObject.tag == "cantOverlap"))
        {
            overlapWarning = true;
        }
        else
        {
            overlapWarning = false;
        }
    }

    public void PutItUp()
    {
        GetComponent<MeshRenderer>().material = undown;
        GetComponent<Collider>().isTrigger = true;
        isPickUp = true;
    }

    public void PutItDown()
    {
        isPickUp = false;
        GetComponent<MeshRenderer>().material = down;
        GetComponent<Collider>().isTrigger = false;
    }
}
