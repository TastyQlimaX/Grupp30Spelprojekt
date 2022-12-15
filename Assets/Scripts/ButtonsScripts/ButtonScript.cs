using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool isPressed;
    public string nameOfTag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Tags>(out var tags))
        {
            if (tags.HasTag(nameOfTag))
            {
                isPressed = true; 
            }
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        isPressed = false;
    }
}
