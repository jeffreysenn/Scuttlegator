using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public float housetimer;
    private GameObject house;
    private bool locked;

    private void Start()
    {
        house = GameObject.FindGameObjectWithTag("House");
        Invoke("lockCamera", housetimer);
    }


    private void Update()
    {
        if (!locked)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, house.transform.position.y + 10.0f, Camera.main.transform.position.z);
        }
        

    }


    private void lockCamera()
    {
        locked = true;
    }
}
