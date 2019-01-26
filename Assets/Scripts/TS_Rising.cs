using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS_Rising : MonoBehaviour
{
    [SerializeField] private float risingSpeed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.up * risingSpeed;
    }
}
