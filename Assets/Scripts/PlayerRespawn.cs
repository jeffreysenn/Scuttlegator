using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{

    public float spawnOffset = 2.0f;
    public float forcedRespawnDistance = 10.0f;
    GameObject house;

    // Start is called before the first frame update
    void Start()
    {
        house = GameObject.FindGameObjectWithTag("House");
    }

    // Update is called once per frame
    void Update()
    {
        if (house.transform.position.y - transform.position.y > forcedRespawnDistance 
            || Input.GetKeyUp(KeyCode.R))
        {
            Vector3 spawnPos = house.transform.position;
            spawnPos.y += spawnOffset;
            transform.position = spawnPos;
        }
    }
}
