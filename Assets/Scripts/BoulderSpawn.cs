using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawn : MonoBehaviour
{
    public GameObject boulder;
    private bool _hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _hasSpawned == false)
        {
            SpawnBoulder();
        }
    }

    private void SpawnBoulder()
    {
        Instantiate(boulder, transform.position, Quaternion.identity);
        _hasSpawned = true;
        Destroy(gameObject);
    }
}
