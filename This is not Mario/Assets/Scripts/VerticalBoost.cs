using System.Collections;
using UnityEngine;


public class VerticalBoost : MonoBehaviour
{

    public GameObject Mario;
    Rigidbody2D rigid;

    bool spawn=false;
    

    void OnTriggerStay2D(Collider2D obstacle)
    {
        if  (obstacle.gameObject.tag == "Player")
        { 
            if (spawn == false)
            {

                rigid = Mario.GetComponent<Rigidbody2D>();

                rigid.velocity = new Vector2(0, 250f);

                spawn = true;

                
            }
        }

    }
}

