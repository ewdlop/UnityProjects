using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontrol : MonoBehaviour {
    
    private Transform myTransform;
    private float gameWidth=6;

	// Use this for initialization
	void Start () {
        myTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        float moveInputAmount = Input.GetAxis("Horizontal");
        if (moveInputAmount > 0 && myTransform.position.x <gameWidth)
        {
            myTransform.Translate(new Vector2(0.1f, 0));
        }
        if (moveInputAmount < 0 && myTransform.position.x > -1*gameWidth)
        {
            myTransform.Translate(new Vector2(-0.1f, 0));
        }
    }
}
