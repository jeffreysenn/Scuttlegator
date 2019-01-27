using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public Texture2D fadeTexture;
    float fadeSpeed = 0.05f;
    int drawDepth = -1000;
 
    private float alpha = 1.0f; 
    private float fadeDir = -1;
 
    
    private void OnGUI(){
        alpha += fadeDir * fadeSpeed * Time.deltaTime;  
        alpha = Mathf.Clamp01(alpha);   
     
        Color newColor = GUI.color; 
        newColor.a = alpha;
        
        GUI.color = newColor;
     
        GUI.depth = drawDepth;
     
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
    }
}
