using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftandRight : MonoBehaviour {

    public float distance;
    float begin;
    public bool reversed;
    public bool y;

    void Start()
    {
        if (y)
        {
            begin = transform.position.y;

        }
        else
        {
            begin = transform.position.x;
        }
    }
    void Update()
    {
        if (y)
        {
            if (reversed)
            {
                transform.position = new Vector3(transform.position.x, begin + -1 * Mathf.PingPong(Time.time * 10, distance), transform.position.z);
            }
            else
            {

                transform.position = new Vector3(transform.position.x, begin + Mathf.PingPong(Time.time * 10, distance), transform.position.z);
            }
        }
        else
        {
            if (reversed)
            {
                transform.position = new Vector3(begin + -1 * Mathf.PingPong(Time.time * 10, distance), transform.position.y, transform.position.z);
            }
            else
            {

                transform.position = new Vector3(begin + Mathf.PingPong(Time.time * 10, distance), transform.position.y, transform.position.z);
            }
        }
    }
    }

