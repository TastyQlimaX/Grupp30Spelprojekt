using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{

    private Rigidbody capsuleRB;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    
    
    [Header("Movement")]
    public float speed = 5f;
    
    public float groundDrag;
    

    [Header("Ground Check")] 
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;
    
    
    private void Awake()
    {
        capsuleRB = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += jump;
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        capsuleRB.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * (speed * 10f), ForceMode.Force);
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (grounded)
        {
            Debug.Log("TEsting");
            capsuleRB.drag = groundDrag;
        }
        else capsuleRB.drag = 0;
    }


    public void jump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        {
            Debug.Log("jump!" + context.phase);
            capsuleRB.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}
