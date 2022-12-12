using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    //get references
    private Rigidbody _capsuleRb;
    private PlayerInputActions playerInputActions;
    public Animator animator;
    
    //Parameters for movement
    [Header("Movement")]
    private float moveSpeed;
    public float sprintSpeed;
    public float walkSpeed;
    public bool Issprinting = false;

    public float dashSpeed;
    public bool dashing;
    public Vector3 startPos;
    
    
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        dashing,
        air
    }


    //parameters for Raycasting
    [Header("Ground Check")] 
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    
    
    public void Awake()
    {
        _capsuleRb = GetComponent<Rigidbody>();
        GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Sprint.performed += Sprint;
        startposition();
    }
    
  

    private void StateHandler(bool isSprintingPressed)
    {
        //Mode - Dashing
        if (dashing)
        {
            state = MovementState.dashing;
            moveSpeed = dashSpeed;
        }
        //Mode -Sprinting
        else if (grounded && isSprintingPressed)
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
            
        }
        //Mode - walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
        
    }

    public void startposition()
    {
        startPos = _capsuleRb.position;
    }
    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        StateHandler(Issprinting);
        
        //adds drag when on ground and not dashing
        if(grounded)
            _capsuleRb.AddForce(new Vector3(inputVector.x, 0, inputVector.y).normalized * (moveSpeed * 10f), ForceMode.Force); 
        //in air
        else
            _capsuleRb.AddForce(new Vector3(inputVector.x, 0, inputVector.y).normalized * (moveSpeed * 10f * airMultiplier), ForceMode.Force);
        if (_capsuleRb.position.y < -50)
        {
            _capsuleRb.position = startPos;
        }
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Issprinting = !Issprinting;
        }
    }

    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
        SpeedControl();
        //handle drag
        if (state == MovementState.walking || state == MovementState.sprinting || state == MovementState.crouching)
        {
            _capsuleRb.drag = groundDrag;
        }
        if(!grounded) _capsuleRb.drag = 0;
        
        
        animator.SetFloat("Horizontal", _capsuleRb.velocity.x);
        animator.SetFloat("Vertical", _capsuleRb.velocity.z);
        animator.SetFloat("Speed", _capsuleRb.velocity.sqrMagnitude);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_capsuleRb.velocity.x, 0f, _capsuleRb.velocity.z);
        
        //Limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            _capsuleRb.velocity = new Vector3(limitedVel.x, _capsuleRb.velocity.y, limitedVel.z);
        }
    }


    private void Jump(InputAction.CallbackContext context)
    {
        
        //checks if jump button assigned in input system has been pressed
        if (context.performed && readyToJump && grounded)
        {
            //makes y = 0 to make sure jump is always the same height
            _capsuleRb.velocity = new Vector3(_capsuleRb.velocity.x, 0f, _capsuleRb.velocity.z); 
            readyToJump = false;
            _capsuleRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
