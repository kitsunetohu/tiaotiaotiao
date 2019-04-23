using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class PlayerController : MonoBehaviour
{
    Animator playerAnimator;
    public enum PlayerStates{
        wait,
        run,
        jump,
        die,
    }

    StateMachine<PlayerStates> playerFsm;

    void Awake (){
         playerFsm=StateMachine<PlayerStates>.Initialize(this);
         
    }
    void Start(){
        playerAnimator=GetComponent<Animator>();
        playerFsm.ChangeState(PlayerStates.wait);
    }

    void wait_Enter(){
        playerAnimator.Play("Standing@loop");
    }
    void wait_Updata(){
        if(Input.GetAxisRaw("Horizontal")!=0){
            
        }
    }

}
