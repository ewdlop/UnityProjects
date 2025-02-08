using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour {


    float x;
    float y;
    Rigidbody2D rigid;
    BoxCollider2D[] box;
    BoxCollider2D mariobox;
    Animator anim;
    public GameObject musicplayer;
    public static bool Johncena = false;
    public static bool eaten = false;



    private void Awake()
    {

        y = transform.position.y;
        rigid = GetComponent<Rigidbody2D>();


    }


    void Update()
    {
        if (transform.position.y >= y + 1.3f)
        {
            box = GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D b in box) b.enabled = true;

            rigid.velocity = new Vector2(0, 0);
        }

        /**if (Johncena == true)
        {
            Vector2 resize= gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
            mariobox.size = resize;
        }**/


    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            mariobox = collision.gameObject.GetComponent<BoxCollider2D>();
            mariobox.size = new Vector2(0.1234274f, 0.1901461f);
            mariobox.GetComponent<CharacterControllers>().maxSpeed = 150f;
            mariobox.GetComponent<CharacterControllers>().jumpforce = 150f;
            collision.gameObject.transform.localScale=new Vector3(2f,2f,collision.gameObject.transform.localScale.z);
            eaten = true;
            DestroyObject(gameObject);


        }
    }
}
