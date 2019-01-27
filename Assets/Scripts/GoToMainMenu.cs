using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{
    public float timer = 10.0f;
    private void Start()
    {
        Invoke("DoItPls", timer);
    }

    public void DoItPls()
    {
        SceneManager.LoadScene(0);
    }
}
