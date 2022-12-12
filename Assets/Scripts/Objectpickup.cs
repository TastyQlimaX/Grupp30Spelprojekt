using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Objectpickup : MonoBehaviour
{
    private PlayerInputActions PlayerInputActions;
    private Rigidbody _capsuleRB;
    
    public GameObject whatIsPickable;
    public GameObject playerPickablePoint;


    public void Awake()
    {
        GetComponent<PlayerInput>();
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Player.Enable();
        PlayerInputActions.Player.Interract.performed += Interract;
    }

    private void PickupObject()
    {
        whatIsPickable.transform.SetParent(playerPickablePoint.transform);
        whatIsPickable.transform.localScale = new Vector3(1f, 1f, 1f);

    }

    private void Interract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            whatIsPickable.transform.SetParent(playerPickablePoint.transform);
            whatIsPickable.transform.localPosition = new Vector3(0f, 0f, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickable"))
        {
            Debug.Log("tag is pickable");
            whatIsPickable = other.gameObject;
        } 
    }
}
