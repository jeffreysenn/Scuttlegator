using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    
    [Range(1, 9)]
    public uint AmountOfBackgrounds;

    public Sprite BackgroundSprite;
    public GameObject[,] BackgroundObjects;

    public float parallaxWeight;
    public Color SpriteColor;
    public int OrderInLayer;

    private GameObject House;
    private float SpriteHeight;
    private float SpriteWidth;

    private Vector3 lastCamPos;

    private void Start()
    {
        lastCamPos = Camera.main.transform.position;

        if (AmountOfBackgrounds % 2 == 0) AmountOfBackgrounds++;
        BackgroundObjects = new GameObject[AmountOfBackgrounds,AmountOfBackgrounds];

        House = GameObject.FindGameObjectWithTag("House");
        if (!House) Debug.LogError("House not found!");

        for (int i = 0; i < AmountOfBackgrounds; i++)
        {
            for (int j = 0; j < AmountOfBackgrounds; j++)
            {
                {
                    BackgroundObjects[i,j] = new GameObject();
                    SpriteRenderer sr = BackgroundObjects[i, j].AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
                    sr.sprite = BackgroundSprite;
                    sr.color = SpriteColor;
                    sr.sortingOrder = OrderInLayer;
                }
            }
            
        }

        SpriteWidth = BackgroundSprite.bounds.size.x;
        SpriteHeight = BackgroundSprite.bounds.size.y;


        Vector2 origin = House.transform.position;
        origin.x -= (float)AmountOfBackgrounds / (float)2.0f * SpriteWidth;
        origin.y -= (float)AmountOfBackgrounds / (float)2.0f * SpriteHeight;

        for (int i = 0; i < AmountOfBackgrounds; i++)
        {
            for (int j = 0; j < AmountOfBackgrounds; j++)
            {
                Vector3 pos = new Vector3(
                    origin.x + i * SpriteWidth + SpriteWidth / 2.0f, 
                    origin.y + j * SpriteHeight + SpriteHeight / 2.0f, 
                    0);
                BackgroundObjects[i, j].transform.position = pos;
            }
        }

    }

    private void Update()
    {
        Vector3 campos = Camera.main.transform.position;

        Vector3 delta = campos - lastCamPos;
        if (delta.magnitude != 0)
        {
            for (int i = 0; i < AmountOfBackgrounds; i++)
            {
                for (int j = 0; j < AmountOfBackgrounds; j++)
                {
                    GameObject g = BackgroundObjects[i, j];
                    g.transform.position += delta * parallaxWeight;

                    //loop back
                    if (g.transform.position.x < Camera.main.transform.position.x - ((float)AmountOfBackgrounds / 2.0f + 1.0f) * SpriteWidth)
                    {
                        g.transform.position = new Vector3(g.transform.position.x + AmountOfBackgrounds * SpriteWidth, g.transform.position.y, g.transform.position.z);
                    }

                    if (g.transform.position.x > Camera.main.transform.position.x + ((float)AmountOfBackgrounds / 2.0f + 1.0f) * SpriteWidth)
                    {
                        g.transform.position = new Vector3(g.transform.position.x - AmountOfBackgrounds * SpriteWidth, g.transform.position.y, g.transform.position.z);
                    }

                    if (g.transform.position.y < Camera.main.transform.position.y - ((float)AmountOfBackgrounds / 2.0f + 1.0f) * SpriteWidth)
                    {
                        g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y + AmountOfBackgrounds * SpriteWidth, g.transform.position.z);
                    }

                    if (g.transform.position.y > Camera.main.transform.position.y + ((float)AmountOfBackgrounds / 2.0f + 1.0f) * SpriteWidth)
                    {
                        g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y - AmountOfBackgrounds * SpriteWidth, g.transform.position.z);
                    }
                }
            }
        }
        

        

        lastCamPos = campos;
    }
}
