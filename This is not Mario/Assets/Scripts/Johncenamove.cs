using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Johncenamove : MonoBehaviour {


    float x;
    float y;
    Rigidbody2D rigid;
    BoxCollider2D [] box;
    BoxCollider2D mariobox;
    Animator anim;
    public GameObject musicplayer;
    public static bool Johncena =false;
    public static bool eaten=false;
    public bool isJohncena;


    private void Awake()
    {

        y = transform.position.y;
        rigid = GetComponent<Rigidbody2D>();

        
    }


    void Update () {
		if(transform.position.y>=y+1.3f)
        {
            box = GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D b in box) b.enabled = true;

            rigid.linearVelocity = new Vector2(0, 0);
        }

        /**if (Johncena == true)
        {
            Vector2 resize= gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
            mariobox.size = resize;
        }**/

        
    }

    void OnTriggerStay2D(Collider2D collision )
    {
        if(collision.gameObject.tag=="Player"){

            if(isJohncena)
            {
                Instantiate(musicplayer, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
                anim = collision.gameObject.GetComponent<Animator>();
                mariobox = collision.gameObject.GetComponent<BoxCollider2D>();
                anim.SetBool("JohnCena", true);
                Johncena = true;
                mariobox.size = new Vector2(0.161f, 0.319f);
                eaten = true;
                collision.gameObject.tag = "JohnCena";
                DestroyObject(gameObject);
            }
            else
            {
                eaten = true;
                DestroyObject(gameObject);

            }

            

        }
    }
}
