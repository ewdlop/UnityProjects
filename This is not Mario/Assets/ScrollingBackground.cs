using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {


    public float scrollspeed;
    public float backgroundsize;
    public Camera cam;

    Vector3 startposition;

	void Start () {
        
        startposition = new Vector3(0f-backgroundsize- cam.orthographicSize * cam.aspect, transform.position.y,transform.position.z);
        transform.position = startposition;
    }
	

	void Update () {

        float newPositiom = Mathf.Repeat(Time.time * scrollspeed,(backgroundsize+ cam.orthographicSize * cam.aspect)*2);
        transform.position = startposition + new Vector3(newPositiom,0, 0);

    }
}
