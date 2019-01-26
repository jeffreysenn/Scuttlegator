using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowHouse : MonoBehaviour
{
    public GameObject house;
    private Vector3 offset;
    private void Start()
    {
        if (!house) Debug.LogError("house not set");
        offset = -house.transform.position + transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = house.transform.position + offset;
    }
}
