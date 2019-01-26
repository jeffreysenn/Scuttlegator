using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("House"))
        {
            Invoke("win",3);
            
        }
    }

    private void win()
    {
        SceneManager.LoadScene("WinScene");
    }
}
