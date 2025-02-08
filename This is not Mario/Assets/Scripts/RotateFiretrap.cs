using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFiretrap : MonoBehaviour {

    public float speed;
    public bool reversed;

	// Update is called once per frame
	void Update () {

        if (reversed)
        {
            transform.Rotate(new Vector3(0, 0, Time.deltaTime*speed*50));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, -1*Time.deltaTime*speed*50));
        }

    }
}
