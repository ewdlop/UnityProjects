using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGround : MonoBehaviour {

    public GameObject left ;
    public GameObject right;


    Rigidbody2D rigid1;
    Rigidbody2D rigid2;

    bool step = false;



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&&step==false)
        {
            if (left.GetComponent<Rigidbody2D>() != null)
            {
                rigid1 = left.GetComponent<Rigidbody2D>();
                DestroyObject(left);
            }
            else
            {
                rigid1 = left.AddComponent<Rigidbody2D>();
                rigid1.AddForce(new Vector2(0, -100f));
            }

            if (right.GetComponent<Rigidbody2D>() != null)
            {
                
                rigid2 = right.GetComponent<Rigidbody2D>();
                DestroyObject(right);
            }
            else
            {
                rigid2 = right.AddComponent<Rigidbody2D>();
                
                rigid2.AddForce(new Vector2(0, -100f));
            }
            step = true;



            DestroyObject(gameObject);
        }
    }

}
