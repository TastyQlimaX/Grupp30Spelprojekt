using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dashing : MonoBehaviour
{
    [Header("References")] public Transform orientation;
    private Rigidbody rb;
    private PlayerController pc;
    private PlayerInputActions playerInputActions;

    [Header("Dashing")] 
    public float dashForce;
    public float dashDuration;

    [Header("Cooldown")] 
    public float dashCD;
    private float dashCDTimer;

    private void Awake()
    {
        GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Dash.performed += Dash;
        playerInputActions.Player.Enable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pc = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (dashCDTimer > 0)
            dashCDTimer -= Time.deltaTime;
    }

    private void Dash(InputAction.CallbackContext context)
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        Vector3 forceToApply =  new Vector3(inputVector.x, 0f, inputVector.y)* dashForce;
        
        
        if (context.performed)
        {
            if (dashCDTimer > 0) return;
            dashCDTimer = dashCD;
            pc.dashing = true;
            // Adds small delay to avoid problem where dashforce is applied before dashing mode is switched to
            delayedForceToApply = forceToApply;
            Invoke(nameof(DelayedDashForce), 0.025f);
            Invoke(nameof(ResetDash), dashDuration);
        }
    }

    private Vector3 delayedForceToApply;
    
    private void DelayedDashForce()
    {
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }
    private void ResetDash()
    {
        pc.dashing = false;
    }
}
