﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
        base.Awake();
        fsm = StateMachine<GameState>.Initialize(this);
        fsm.ChangeState(GameState.Prepare);
        if (UserWin == null)
        {
            UserWin = new UnityEvent();

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

    public void ReplayScene()
    {
        Debug.Log("replay");
        SceneManager.LoadScene("SampleScene");
        UIManger.Instance.Init();
        fsm.ChangeState(GameState.Rurrning);
    }

    public void NextScene(){
        Debug.Log("load next scene");
    }
}