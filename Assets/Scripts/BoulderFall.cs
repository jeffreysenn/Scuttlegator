using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderFall : MonoBehaviour
{
    private Rigidbody2D body;

    public bool left = false;
    public bool right = false;
    [SerializeField] private float initialForce;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();


        if (right)
        {
            body.AddForce(new Vector2(initialForce,200), ForceMode2D.Force);

        }

        if (left)
        {
            body.AddForce(new Vector2(-initialForce,200), ForceMode2D.Force);

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //bouncy boulder
        //throw new System.NotImplementedException();
    }

    void Update()
    {
        
    }
}
