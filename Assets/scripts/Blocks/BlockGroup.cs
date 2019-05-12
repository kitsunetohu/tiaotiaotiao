using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGroup : MonoBehaviour//ブロックというクラスの特徴はユーザーに放置されることができる
{

    //经过后是否消失
    public bool canThrough;
    
    public GameObject itSelf;
    public GameObject ghost;
    public GameObject overlapGhost;
    //空物体下面挂三个物体，什么状态就enable哪个


    bool isPickUp = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected void Update()
    {

        if (isPickUp)//是否被拿起
        {

            if (OverlapWarning())
            {
                if(overlapGhost.activeSelf!=true){
                    UnActiveAllChild();
                    overlapGhost.SetActive(true);
                }
            }
            else
            {   if(overlapGhost.activeSelf==true)
                {
                    UnActiveAllChild();
                    overlapGhost.SetActive(false);
                    ghost.SetActive(true);

                }
                
       
            }

        }
    }

    bool OverlapWarning()
    {
        Collider[] collisions = Physics.OverlapBox(transform.position, itSelf.GetComponent<BoxCollider>().size / 2.0f, transform.rotation);


        foreach (Collider x in collisions)
        {
            if (x.gameObject.CompareTag("Player") || x.gameObject.CompareTag("cantOverlap"))
            {
                if (x.gameObject != gameObject) return true;

            }
        }
        return false;
    }


    public void PutItUp()
    {
        UnActiveAllChild();
        ghost.SetActive(true);
        
        isPickUp = true;

    }

    public bool PutItDown()
    {
        if (OverlapWarning() == true)
        {
            return false;
        }
        isPickUp = false;
        UnActiveAllChild();
        itSelf.SetActive(true);
        
        return true;
    }

    void UnActiveAllChild()
    {
        itSelf.SetActive(false);
        ghost.SetActive(false);
        overlapGhost.SetActive(false);
    }
}
