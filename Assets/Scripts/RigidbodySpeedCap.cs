using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodySpeedCap : MonoBehaviour
{
    public float baseMaxSpeed = 10;
    public float balloonAddSpeedMod = 0.1f;
    public float absoluteMaxSpeed = 15;

    private float targetSpeedCap;
    private Rigidbody2D rgbd;
    // Update is called once per frame
    private void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        targetSpeedCap = baseMaxSpeed + balloonAddSpeedMod * BallonInfo.activeBalloonNum;
        if(targetSpeedCap > absoluteMaxSpeed) { targetSpeedCap = absoluteMaxSpeed; }
        if(rgbd.velocity.magnitude > baseMaxSpeed)
        {
            rgbd.velocity = rgbd.velocity.normalized * baseMaxSpeed;
        }
    }
}
