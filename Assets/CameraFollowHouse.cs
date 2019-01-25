using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowHouse : MonoBehaviour
{
    public GameObject house;
    private void Start()
    {
        if (!house) Debug.LogError("house not set");
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, house.transform.position.y + 4.0f, Camera.main.transform.position.z);
    }
}
