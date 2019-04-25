using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float minSpeed=0.5f;//落地缓冲时的速度
    public float JumpHeight = 2f;
    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public LayerMask Ground;

    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private Transform _groundChecker;
    private float Speed;
  


    Animator playerAnimator;
    public enum PlayerStates
    {
        wait,
        run,
        jump,
        die,
    }

    StateMachine<PlayerStates> playerFsm;

    void Awake()
    {
        playerFsm = StateMachine<PlayerStates>.Initialize(this);

    }
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerFsm.ChangeState(PlayerStates.wait);
        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
        Speed=maxSpeed;
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);


        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxisRaw("Horizontal");

        if (_inputs != Vector3.zero)
            transform.forward = _inputs;

    }
    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }

    void wait_Enter()
    {
        playerAnimator.Play("Standing@loop");
    }
    void wait_Update()
    {

        if (_inputs.x != 0)
        {
            playerFsm.ChangeState(PlayerStates.run);
        }
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            playerFsm.ChangeState(PlayerStates.jump);
        }
    }


    void run_Enter()
    {
        playerAnimator.Play("Running@loop");
    }
    void run_Update()
    {
        if (_inputs.x == 0)
        {
            playerFsm.ChangeState(PlayerStates.wait);
        }
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {           
            playerFsm.ChangeState(PlayerStates.jump);
        }
    }

    void jump_Enter()
    {
        playerAnimator.SetBool("Land",false);
        playerAnimator.Play("JumpToTop");
    }
    void jump_Update()
    {

        if (_body.velocity.y < 0)
        {
            
            if (_isGrounded)
            {//马上就要着陆,跳起动画状态下模型会比collider高
                playerAnimator.SetBool("Land",true);   
                Speed=minSpeed;
            }
           
        }
    }

    public void ChargeOver(){
       _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }
    public void Land(){
        Speed=maxSpeed;
        playerFsm.ChangeState(PlayerStates.wait);
    }
}
