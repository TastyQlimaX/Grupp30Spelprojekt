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
    public GameObject Light;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Tags>(out var tags))
        {
            if (tags.HasTag(nameOfTag))
            {
                isPressed = true;
                buttonSFX.clip = Pushed;
                buttonSFX.Play();
                Light.SetActive(true);
            }

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Tags>(out var tags))
        {
            if (tags.HasTag(nameOfTag))
            {
                isPressed = false;
                buttonSFX.clip = Pushed;
                Light.SetActive(false);
            }

        }
    }
}
