using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool isPressed;
    public string nameOfTag;
    public AudioSource buttonSFX;
    public AudioClip Pushed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Tags>(out var tags))
        {
            if (tags.HasTag(nameOfTag))
            {
                isPressed = true;
                buttonSFX.clip = Pushed;
                buttonSFX.Play();
            }
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        isPressed = false;
    }
}
