using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPopping : MonoBehaviour
{
    public GameObject PoppedBalloon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Balloon"))
        {
            BallonInfo.activeBalloonNum--;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Balloon"))
        {
            BallonInfo.activeBalloonNum--;

            GameObject g = Instantiate(PoppedBalloon, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            g.GetComponent<Rigidbody2D>().velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;

            Destroy(collision.gameObject);
        }
    }
}
