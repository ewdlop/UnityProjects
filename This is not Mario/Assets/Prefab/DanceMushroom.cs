using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceMushroom : MonoBehaviour {

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

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DestroyObject(gameObject);
            GameObject thriller = GameObject.Find("Thriller");
            collision.gameObject.GetComponent<CharacterControllers>().maxSpeed = -20f;
        }
    }
    void Update()
    {
        if (transform.position.y >= y + 1.3f)
        {
            box = GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D b in box) b.enabled = true;

            rigid.velocity = new Vector2(0, 0);
        }


    }
}
