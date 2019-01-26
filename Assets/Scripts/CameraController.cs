using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject house;
    [SerializeField] float maxShakeMod = 1;
    [SerializeField] float noShakeSpeed = 8;

    private Vector3 offset;


    private Rigidbody2D houseRgbody;
    private RigidbodySpeedCap houseSpeedCap;
    private Vector3 preVel;
    private Vector3 acc;
    private Vector3 originalPos;

    private void Start()
    {
        if (!house) Debug.LogError("house not set");

        houseRgbody = house.GetComponent<Rigidbody2D>();
        houseSpeedCap = house.GetComponent<RigidbodySpeedCap>();

        offset = -house.transform.position + transform.position;

        preVel = houseRgbody.velocity;

        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float targetShakeMod;
        if(preVel.magnitude < noShakeSpeed) { targetShakeMod = 0; }
        else
        {
            targetShakeMod = maxShakeMod * (preVel.magnitude - noShakeSpeed) / (houseSpeedCap.absoluteMaxSpeed - noShakeSpeed);
        }
        transform.localPosition = house.transform.position + offset + Random.insideUnitSphere * targetShakeMod;
    }

    private void FixedUpdate()
    {
        Vector3 currentVel = houseRgbody.velocity;
        acc = (currentVel - preVel) / Time.fixedDeltaTime;
        preVel = currentVel;
    }
}
