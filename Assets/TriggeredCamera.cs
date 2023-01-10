using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredCamera : MonoBehaviour
{
    public GameObject camMain;

    public GameObject camCut;
    // Start is called before the first frame update
    void Start()
    {
        camMain.SetActive(true);
        camCut.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        camCut.SetActive(true);
        camMain.SetActive(false);
    }
}
