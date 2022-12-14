using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsActionScript4B : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;

    private ButtonScript button1Script;
    private ButtonScript button2Script;
    private ButtonScript button3Script;
    private ButtonScript button4Script;
    // Start is called before the first frame update
    void Start()
    {
        button1Script = button1.GetComponent<ButtonScript>();
        button2Script = button2.GetComponent<ButtonScript>();
        button3Script = button3.GetComponent<ButtonScript>();
        button4Script = button4.GetComponent<ButtonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (button1Script.isPressed && button2Script.isPressed && button3Script.isPressed && button4Script.isPressed)
        {
            Debug.Log("All buttons are pressed. Someting will happen here");
            Destroy(gameObject);
        }
    }
}