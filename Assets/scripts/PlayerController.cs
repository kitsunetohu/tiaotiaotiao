using System.Collections;
using System.Collections.Generic;
using MonsterLove.StateMachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float minSpeed = 0.5f; //落地缓冲时的速度
    public float JumpHeight = 2f;
    public float GroundDistance = 0.1f;
    public float DashDistance = 5f;
    public LayerMask Ground;
    public float speedToHighest = 1;
    public Transform _groundChecker; //选脚

    [SerializeField, Range(0, 1)]
    public float animaSpeedInSky;

    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;

    private float Speed;
    private bool isJumping = false;
    CapsuleCollider capsuleCollider;

    float yBeforJump;

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
        //_groundChecker = transform.GetChild(0);
        Speed = maxSpeed;
        capsuleCollider = GetComponent<CapsuleCollider>();   
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxisRaw("Horizontal");

        if (_inputs != Vector3.zero)
            transform.forward = _inputs;

        if(Input.GetKey(KeyCode.LeftShift)){
            GetComponent<Rigidbody>().AddForce(DashDistance*transform.forward, ForceMode.Force);
        }


    }
    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }

    void wait_Enter()
    {
        playerAnimator.CrossFade("Standing@loop", 0.1f, 0, 0.2f);
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
        playerAnimator.CrossFade("Running@loop", 0.1f, 0, 0.2f);
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

        playerAnimator.CrossFade("Jumping@loop", 0.05f, 0, 0.05f);
        StartCoroutine(JumpCharge());

    }
    void jump_Update()
    {

        if (_isGrounded && isJumping) //キャラが空中に飛ぶあと、着地する時発動する
        {
            Land();

            playerFsm.ChangeState(PlayerStates.wait);

        }
        _body.AddForce(DashDistance*transform.forward, ForceMode.Force);


    }

    IEnumerator JumpCharge()
    {
        //ジャンプする前に遅延があるので、チャレンジ完了あと、YUKOにスピードを与える
        yield return new WaitForSeconds(0.1f);
        _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }
    void jump_Exit()
    {
        isJumping = false;
    }

    public void Land()
    {
        Debug.Log("land");
        playerAnimator.speed = 1;
        Speed = maxSpeed;
        if (_inputs.x == 0)
        {
            playerFsm.ChangeState(PlayerStates.wait);
        }
        else
        {
            playerFsm.ChangeState(PlayerStates.run);

        }

    }

    void changeJumping() //アニメーションにコールバックされる
    {
        isJumping = true;

    }

    void PauseJump()
    {
        if (!_isGrounded)
        {
            playerAnimator.speed = animaSpeedInSky;

        }
    }

    public void Dash(float DashDistance)
    {
        _body.AddForce(DashDistance*transform.forward, ForceMode.Impulse);
       
    }
}