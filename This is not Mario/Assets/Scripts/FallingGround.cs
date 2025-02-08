using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGround : MonoBehaviour {

    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void OnTriggerStay2D(Collider2D obstacle)
    {

        if (obstacle.gameObject.tag == "Player")
        {
            rigid.AddForce(new Vector2(0, -500));


        }

    }
}
