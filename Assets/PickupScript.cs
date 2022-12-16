using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupScript : MonoBehaviour
{
    private PlayerInputActions PlayerInputActions;
    private Rigidbody PickableRB;
    private Rigidbody PlayerRB;

    public GameObject StoredObj;
    public GameObject whatIsPickable;
    public GameObject playerPickablePoint;
    public GameObject Player;
    public bool isPickedUp = false;
    public int throwStrength;
    public int pickupDrag;

    public void Awake()
    {
        GetComponent<Rigidbody>();
        GetComponent<PlayerInput>();
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Player.Enable();
        PlayerInputActions.Player.Interract.performed += Interact;
        PlayerRB = Player.GetComponent<Rigidbody>();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        
        
        if (context.performed && whatIsPickable.CompareTag("Pickable")&& !isPickedUp)
        {
            StoredObj = whatIsPickable;
            isPickedUp = true;
            Destroy(StoredObj.GetComponent<Rigidbody>());
            StoredObj.transform.SetParent(playerPickablePoint.transform);
            StoredObj.transform.localPosition = new Vector3(0f, 1f, 1f);
            
        }

        else if (context.performed && isPickedUp)
        {
            StoredObj.AddComponent<Rigidbody>();
            PickableRB = StoredObj.GetComponent<Rigidbody>();
            PickableRB.constraints = RigidbodyConstraints.FreezeRotation;
            PickableRB.drag = pickupDrag;
            StoredObj.transform.parent = null;
            isPickedUp = false;
            PickableRB.AddForce(new Vector3(PlayerRB.velocity.x, PlayerRB.velocity.y, PlayerRB.velocity.z) * throwStrength, ForceMode.Impulse);
            StoredObj = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickable"))
        {
            whatIsPickable = other.gameObject;

        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickable") && !isPickedUp)
        {
            whatIsPickable = null; 
        }

    }
}
