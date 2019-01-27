using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonDeath : MonoBehaviour
{
    public GameObject PoppedBalloon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameObject g = Instantiate(PoppedBalloon, transform.position, transform.rotation);
        g.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
    }
}
