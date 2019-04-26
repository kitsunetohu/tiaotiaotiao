using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float minSpeed = 0.5f;//落地缓冲时的速度
    public float JumpHeight = 2f;
    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public LayerMask Ground;
    public float speedToHighest = 1;
    public Transform _groundChecker;//选脚

    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;

    private float Speed;
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

    }
    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }

    void wait_Enter()
    {
        playerAnimator.CrossFade("Standing@loop", 0.2f, 0, 0.8f);
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
        playerAnimator.CrossFade("Running@loop", 0.2f, 0, 0.8f);
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
        playerAnimator.SetBool("Land", false);
        playerAnimator.Play("JumpToTop");
    }
    void jump_Update()
    {
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("TopOfJump")){
            playerAnimator.applyRootMotion=false;
            capsuleCollider.center = new Vector3(capsuleCollider.center.x, 1.2f, capsuleCollider.center.z);
        }
        else{
            playerAnimator.applyRootMotion=true;
            capsuleCollider.center = new Vector3(capsuleCollider.center.x, 0.58f, capsuleCollider.center.z);
        }


        if (_body.velocity.y < -2.0f)
        {

            Debug.Log(_body.velocity.y);
            if (_isGrounded)
            {
                Speed = minSpeed;
                playerAnimator.SetBool("Land", true);
              // capsuleCollider.center = new Vector3(capsuleCollider.center.x, 0.58f, capsuleCollider.center.z);

            }

        }

        if (!_isGrounded)
        {
            //capsuleCollider.center = new Vector3(capsuleCollider.center.x, 1.2f, capsuleCollider.center.z);//移动中心使碰撞体吻合
        }


    }

    public void ChargeOver()
    {
        _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }
    public void Land()
    {
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
}
