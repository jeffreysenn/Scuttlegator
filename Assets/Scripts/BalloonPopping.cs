using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPopping : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Balloon"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Balloon"))
        {
            Destroy(collision.gameObject);
        }
    }
}
