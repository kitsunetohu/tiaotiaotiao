using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class GameManger : Manager<GameManger>
{
    bool gameResult;//Is player win when game over？
    public enum GameState{
        Prepare,
        Rurrning,
        Result,

    }
    StateMachine<GameState> fsm;

    new void Awake(){
        
        fsm = StateMachine<GameState>.Initialize(this);
        fsm.ChangeState(GameState.Prepare);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getGameResult(bool isWin){

    }
}
