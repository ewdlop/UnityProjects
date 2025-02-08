using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour {

    public GameObject[] sprites;
    public GameObject mario;
    public bool change;
    public Camera cam;

    void Awake()
    {
        if (change) { 
        sprites = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject objects in sprites)
        {
            if (objects.GetComponent<SpriteRenderer>() != null)
            {
                objects.GetComponent<SpriteRenderer>().color = Color.HSVToRGB((objects.transform.position.x+98f) / (98f+263f), 1f, 1f);
            }

        }
        }

    }
    void Update()
    {
        if (change)
        {
            mario.GetComponent<SpriteRenderer>().color = Color.HSVToRGB((mario.transform.position.x + 98f) / (98f + 263f), 1f, 1f);
            cam.GetComponent<Camera>().backgroundColor= Color.HSVToRGB((mario.transform.position.x + 98f) / (98f + 263f), 1f, 1f);
        }
       
    }
}
