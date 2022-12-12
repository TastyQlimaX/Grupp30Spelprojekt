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
    
    public GameObject whatIsPickable;
    public GameObject playerPickablePoint;
    public GameObject Player;
    public bool isPickedUp;
    public int throwStrength;
    public int pickupDrag;

    public void Awake()
    {
        GetComponent<Rigidbody>();
        GetComponent<PlayerInput>();
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Player.Enable();
        PlayerInputActions.Player.Interract.performed += Interract;
        PlayerRB = Player.GetComponent<Rigidbody>();
    }

    private void PickupObject()
    {
        whatIsPickable.transform.SetParent(playerPickablePoint.transform);
        whatIsPickable.transform.localScale = new Vector3(1f, 1f, 1f);

    }

    private void Interract(InputAction.CallbackContext context)
    {
        if (context.performed && whatIsPickable.CompareTag("Pickable")&& !isPickedUp)
        {
            isPickedUp = true;
            whatIsPickable.transform.SetParent(playerPickablePoint.transform);
            whatIsPickable.transform.localPosition = new Vector3(0f, 1f, 0f);
            Destroy(whatIsPickable.GetComponent<Rigidbody>());
        }

        else if (context.performed && isPickedUp)
        {
            whatIsPickable.AddComponent<Rigidbody>();
            PickableRB = whatIsPickable.GetComponent<Rigidbody>();
            PickableRB.constraints = RigidbodyConstraints.FreezeRotation;
            PickableRB.drag = pickupDrag;
            whatIsPickable.transform.parent = null;
            isPickedUp = false;
            PickableRB.AddForce(new Vector3(PlayerRB.velocity.x, PlayerRB.velocity.y, PlayerRB.velocity.z) * throwStrength, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickable"))
        {
            whatIsPickable = other.gameObject;
        } 
    }

}
