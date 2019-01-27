﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterTime : MonoBehaviour
{
    public float LifeTimeInSeconds;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("Death", LifeTimeInSeconds);
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
