using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG : MonoBehaviour {

    Rigidbody2D rigid;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            rigid = GetComponent<Rigidbody2D>();
            rigid.linearVelocity = new Vector3(50f, 0, 0);
        }
    }

}
