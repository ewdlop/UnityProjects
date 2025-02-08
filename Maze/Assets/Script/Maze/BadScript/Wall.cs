using System;
using UnityEngine;

public class Wall: MonoBehaviour
{
    [Header("Coordinate")]
    public float x;
    public float z;

    [Header("Neighbor")]
    public Wall[] walls;
    public Floor[] floors;

    public GameObject[] lights;

    void Start()
    {
        foreach(GameObject light in lights)
        {
            if(UnityEngine.Random.value < 0.1f)            
                light.SetActive(true);
        }
    }
}
