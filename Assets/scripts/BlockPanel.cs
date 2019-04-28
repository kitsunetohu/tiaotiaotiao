using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;


public class BlockPanel : MonoBehaviour
{
    public enum ChoosingState
    {
        Prepare,
        Choosing,
    }

    [SerializeField]
    public LayerMask shelf;
    StateMachine<ChoosingState> fsm;
    GameObject choosingBlock;
    public List<GameObject> blockList;
    public List<int> blockNumber;
    // Start is called before the first frame update
    void Start()
    {
        fsm = StateMachine<ChoosingState>.Initialize(this);
        fsm.ChangeState(ChoosingState.Prepare);
    }

    void Choosing_Enter()
    {
        if (choosingBlock != null)
        {   //put object behind camera
            choosingBlock= Instantiate(choosingBlock,
            Camera.main.gameObject.transform.position - 100 * Camera.main.gameObject.transform.forward,
            Quaternion.identity);
        }

    }

    void Choosing_Update(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,100,shelf)){
            choosingBlock.transform.position=hit.point;
        }
        if(Input.GetMouseButtonUp(0)){
            fsm.ChangeState(ChoosingState.Prepare);
        }

    }
    public void PushButtonDown(int buttonNum)
    {
        choosingBlock = blockList[buttonNum];
        Debug.Log("button" + buttonNum);
        fsm.ChangeState(ChoosingState.Choosing);
    }
}
