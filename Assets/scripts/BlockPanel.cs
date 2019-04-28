using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using UnityEngine.UI;


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
    GameObject chosingBlock;
    Button chosingButton;
    public List<GameObject> blockList;
    public List<int> _blockNumber;
    public List<Button> blockButtons;
    public List<int> blockNumber;
     int buttonNum;
    // Start is called before the first frame update
    void Start()
    {
        fsm = StateMachine<ChoosingState>.Initialize(this);
        fsm.ChangeState(ChoosingState.Prepare);
        InitializeNum();
        RefreshNum();       
    }

    void Choosing_Enter()
    {
        if (chosingBlock != null)
        {   //put object behind camera
            chosingBlock = Instantiate(chosingBlock,
            Camera.main.gameObject.transform.position - 100 * Camera.main.gameObject.transform.forward,
            Quaternion.identity);
        }

    }

    void Choosing_Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, shelf))
        {
            chosingBlock.transform.position = hit.point;
            if (Input.GetMouseButtonUp(0))
            {//松开切换状态
                blockNumber[buttonNum] -= 1;
                RefreshNum();
                fsm.ChangeState(ChoosingState.Prepare);
                chosingBlock = null;
            }
        }


    }
    public void PushButtonDown(int buttonNum)
    {//バトンをタッチしたら、バトンからバトンの番号がもらえます。
        if (blockNumber[buttonNum] <= 0)
        {
            return;//もしブロックがないなら、リターン
        }
        chosingButton = blockButtons[buttonNum];
        chosingBlock = blockList[buttonNum];
        this.buttonNum = buttonNum;
        Debug.Log("button" + buttonNum);
        fsm.ChangeState(ChoosingState.Choosing);
    }

    void RefreshNum()
    {
        try
        {
            for (int i=0;i<blockNumber.Count;i++)
            {
                string buttonNumInText = System.Convert.ToString(blockNumber[i]);
                blockButtons[i].GetComponentInChildren<Text>().text=buttonNumInText;
            }

        }
        catch (System.ArgumentNullException)
        {
            System.Console.WriteLine("String is null");
        }
    }

    void InitializeNum(){
        blockNumber=new List<int>();
        for (int i = 0; i < _blockNumber.Count; i++)
        {//初期化
            blockNumber.Add(_blockNumber[i]) ;
        }
    }
}
