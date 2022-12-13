using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsActionScript : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;

    private ButtonScript button1Script;
    private ButtonScript button2Script;
    // Start is called before the first frame update
    void Start()
    {
        button1Script = button1.GetComponent<ButtonScript>();
        button2Script = button2.GetComponent<ButtonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (button1Script.isPressed && button2Script.isPressed)
        {
            Debug.Log("Both buttons are pressed. Someting will happen here");
        }
    }
}
