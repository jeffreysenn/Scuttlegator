using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{

    public float spawnOffset = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            Vector3 spawnPos = GameObject.FindGameObjectWithTag("House").transform.position;
            spawnPos.y += spawnOffset;
            transform.position = spawnPos;
        }
    }
}
