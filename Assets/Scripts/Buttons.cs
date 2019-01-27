using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject startButton;
    public GameObject controlsButton;
    public GameObject controlsSprite;

    // Start is called before the first frame update
    void Start()
    {
        startButton.SetActive(false);
        controlsSprite.SetActive(false);
    }

    public void showControls()
    {
        startButton.SetActive(true);
        controlsSprite.SetActive(true);
        controlsButton.SetActive(false);
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
}
