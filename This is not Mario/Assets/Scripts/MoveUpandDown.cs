using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpandDown : MonoBehaviour {

    public float height;
    float begin;

    void Start()
    {

        begin = transform.position.y;

    }


	
     void Update () {

        transform.position = new Vector3(transform.position.x,begin+Mathf.PingPong(Time.time*4,height), transform.position.z);    	
	}
}
