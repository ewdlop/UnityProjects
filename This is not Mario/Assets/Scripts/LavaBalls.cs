using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBalls : MonoBehaviour {

    float y;
    Rigidbody2D rigid;
	
	void Start () {

        y = transform.position.y;
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(new Vector3(0,25f,0),ForceMode2D.Impulse);

	}


    void Update () {


        if (transform.position.y<y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            rigid.AddForce(new Vector3(0, 25f, 0), ForceMode2D.Impulse);
        }
        transform.localScale = new Vector3(transform.localScale.x,16*Mathf.Sign(rigid.velocity.y),transform.localScale.z);

	}
}
