using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.InputSystem;

public class FungusBoolTrigger : MonoBehaviour
{
    public Flowchart flowchart;
    public bool pHere;
    private PlayerInputActions PlayerInputActions;
    private bool ePress;
    public string StartPoint;

    public void Awake()
    {
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Player.Enable();
        PlayerInputActions.Player.Interract.performed += Interact;
        flowchart.SetBooleanVariable("NPCSpeaker", false);
        flowchart = FindObjectOfType<Flowchart>();
        pHere = false;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (pHere)
        {
            if (context.performed)
            {
                flowchart.SetBooleanVariable("NPCSpeaker", true);
                flowchart.ExecuteBlock(StartPoint);
            } 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pHere = true;
            ePress = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            flowchart.SetBooleanVariable("NPCSpeaker", true);
            pHere = false;
        }
    }

    public void ChangeBool()
    {
        if (pHere)
        {
            flowchart.SetBooleanVariable("NPCSpeaker", true);
        }
    }
}
