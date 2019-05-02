using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MonsterLove.StateMachine;

public class GameManager : Manager<GameManager>
{
    
    public UnityEvent UserWin;
    bool isWin;//Is player win when game over？
    public enum GameState
    {
        Prepare,
        Rurrning,
        Result,

    }
    StateMachine<GameState> fsm;

    new void Awake()
    {

        fsm = StateMachine<GameState>.Initialize(this);
        fsm.ChangeState(GameState.Prepare);
        if (UserWin == null)
        {
            UserWin = new UnityEvent();
            Debug.Log("123");
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Result_Enter()
    {//发布胜利事件
        UserWin.Invoke();
    }

    public void getGameResult(bool isWin)
    {
        this.isWin = isWin;
        fsm.ChangeState(GameState.Result);
    }
}
