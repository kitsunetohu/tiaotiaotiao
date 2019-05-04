using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostBlock : Block
{

    public float boostDistance;
    // Start is called before the first frame update
    void Start()
    {
        if (canThrough)
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }



    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().Dash(boostDistance);
        }

    }
}
