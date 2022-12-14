using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsActionScript3B : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    private ButtonScript button1Script;
    private ButtonScript button2Script;
    private ButtonScript button3Script;
    // Start is called before the first frame update
    void Start()
    {
        button1Script = button1.GetComponent<ButtonScript>();
        button2Script = button2.GetComponent<ButtonScript>();
        button3Script = button3.GetComponent<ButtonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (button1Script.isPressed && button2Script.isPressed && button3Script.isPressed)
        {
            Debug.Log("All buttons are pressed. Someting will happen here");
            Destroy(gameObject);
        }
    }
}
