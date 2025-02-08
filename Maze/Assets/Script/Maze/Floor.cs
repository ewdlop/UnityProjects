using System;
using UnityEngine;

public class Floor : MonoBehaviour{

    [Header("Coordinate")]
    public float x;
    public float z;
    public int row;
    public int column;
    public float length;
    public float width;

    [Header("Neighbor")]
    public Wall[] walls;
    public Floor[] floors;

    public Floor()
    {

    }

    public Floor(GameObject prefab, float x, float z) {
        this.x = x;
        this.z = z;
    }


}
