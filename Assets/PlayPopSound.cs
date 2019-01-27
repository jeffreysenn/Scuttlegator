using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPopSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySound("BalloonPop", 1.0f, true, 0.1f);
    }
}
