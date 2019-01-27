using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    
    public Texture2D fadeTexture;
    float fadeSpeed = 0.15f;
    int drawDepth = -1000;
 
    private float alpha = 0.0f; 
    private float fadeDir = 1;

    private bool won = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("House"))
        {
            won = true;
            Invoke("win",5);
            
        }
    }

    private void win()
    {
        SceneManager.LoadScene("WinScene");
    }
    
    // FadeInOut
 
    
    private void OnGUI(){
        if (won != true) return;
        alpha += fadeDir * fadeSpeed * Time.deltaTime;  
        alpha = Mathf.Clamp01(alpha);   
     
        
        Color newColor = GUI.color; 
        newColor.a = alpha;
        
        GUI.color = newColor;
     
        GUI.depth = drawDepth;
     
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
    }
    
}
