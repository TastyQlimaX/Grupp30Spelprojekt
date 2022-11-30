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
    public float moveSpeed = 5f;
    
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;


    //parameters for Raycasting
    [Header("Ground Check")] 
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    
    
    private void Awake()
    {
        _capsuleRb = GetComponent<Rigidbody>();
        GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        //on ground
        if(grounded)
            _capsuleRb.AddForce(new Vector3(inputVector.x, 0, inputVector.y).normalized * (moveSpeed * 10f), ForceMode.Force); 
        //in air
        else
            _capsuleRb.AddForce(new Vector3(inputVector.x, 0, inputVector.y).normalized * (moveSpeed * 10f * airMultiplier), ForceMode.Force);
    }

    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
        SpeedControl();
        
        //handle drag
        if (grounded)
        {
            _capsuleRb.drag = groundDrag;
        }
        else _capsuleRb.drag = 0;
        
        
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
