using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Animator anim;
    public float startingSpeed;
    public float moveSpeed;
    public float SwingSpeed;
    public float RunSpeed;
    public Transform orientation;
    Vector3 moveDirection;
    Vector3 move;
    Rigidbody rb;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    public bool Swinging;
    public bool Running;
    RaycastHit hit;
    public bool IsRunning = false;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    public bool doubleJump = true;
    public Dance Dancing;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        ResetJump();
        moveSpeed = startingSpeed;
    }
    public void OnMoving(InputValue _v)
    {
        move = _v.Get<Vector2>();
    }
    public void OnJump()
    {
        TryToJump();
    }
    public void OnRun()
    {
        IsRunning = !IsRunning;
        if(IsRunning) Running = true;
        if(!IsRunning) Running = false;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Move", Mathf.Abs(move.magnitude));
        if(!grounded)
        {
            if(rb.velocity.y < 0f)
            {
                anim.SetBool("IsFalling",true);
                anim.SetBool("IsRising",false);
            }
            if(rb.velocity.y > 0f)
            {
                anim.SetBool("IsFalling",false);
                anim.SetBool("IsRising",true);
            }
        }    
        if(rb.velocity.y == 0f)
        {
            anim.SetBool("IsFalling",false);
            anim.SetBool("IsRising",false);
        }


        //ground checker
        grounded = Physics.Raycast(transform.position, Vector3.down, out hit, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();
        SpeedControl();

        //handles drag
        if(grounded)
        {
            rb.drag = groundDrag;
            Dancing.Ground = hit.transform;
        }
        else
        {
            rb.drag = 0;
        }
        if(grounded)
        {
            doubleJump = true;
        }
    }


    void MyInput()
    {
        if(Running)
        {
            moveSpeed = RunSpeed;
        }
        if(!Running)
        {
            moveSpeed = startingSpeed;
        }
    }
    void TryToJump()
    {
        if(readyToJump && grounded)
        {
            readyToJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        
        else if(doubleJump && !grounded)
        {
            doubleJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    void MovePlayer()
    {
        //finding movement direction
        moveDirection = orientation.forward * move.y + orientation.right * move.x;
        //on ground
        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void Jump()
    {

        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    void ResetJump()
    {
        readyToJump = true;
    }
}
