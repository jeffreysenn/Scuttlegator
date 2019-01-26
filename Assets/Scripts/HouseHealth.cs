using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HouseHealth : MonoBehaviour
{

    private float health;
    private float damage;
    
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        damage = 25;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            if ((health -= damage) <= 0)
            {
                SceneManager.LoadScene("LoseScene");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
