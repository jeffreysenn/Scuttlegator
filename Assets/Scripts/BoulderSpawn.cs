using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawn : MonoBehaviour
{
    public bool left = false;
    public bool right = false;
    public GameObject boulder;
    private bool _hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("Balloon") || other.CompareTag("House")) && _hasSpawned == false)
        {
            SpawnBoulder();
        }
    }

    private void SpawnBoulder()
    {
        if (left)
        {
            Instantiate(boulder, transform.position, Quaternion.identity).GetComponent<BoulderFall>().left = true;
        }

        if (right)
        {
            Instantiate(boulder, transform.position, Quaternion.identity).GetComponent<BoulderFall>().right = true;
        }
       
        _hasSpawned = true;
        Destroy(gameObject);
    }
}
