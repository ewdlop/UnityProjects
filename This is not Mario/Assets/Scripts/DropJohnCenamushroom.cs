using System.Collections;
using UnityEngine;


public class DropJohnCenamushroom : MonoBehaviour
{

    public GameObject JohnCenamushroom;
    Rigidbody2D rigid;

    bool spawn=false;
    

    void OnTriggerStay2D(Collider2D obstacle)
    {
        if  (obstacle.gameObject.tag == "Player")
        { 
            if (spawn == false)
            {

                rigid = JohnCenamushroom.GetComponent<Rigidbody2D>();

                rigid.gravityScale = 20f;

                spawn = true;

                
            }
        }

    }
}

