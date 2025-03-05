using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnCenatooOp : MonoBehaviour {

    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "JohnCena")
        {


            if (gameObject.GetComponent<Rigidbody2D>() == null)
            {
                rigid = gameObject.AddComponent<Rigidbody2D>();
                rigid.gravityScale = 2;

            }
            else
            {
                rigid = GetComponent<Rigidbody2D>();
                rigid.bodyType = RigidbodyType2D.Dynamic;
                rigid.linearVelocity = new Vector3(0, -30f, 0);
            }
        }
    }

}
