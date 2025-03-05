using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnMushroom : MonoBehaviour {


    float x;
    float y;
    Rigidbody2D rigid;
    BoxCollider2D[] box;
    BoxCollider2D mariobox;
    Animator anim;
    GameObject questionbox;
    Animator boxanim;
    public static bool Johncena = false;
    public static bool eaten = false;
    bool retur = false;
    GameObject mario;
   


    private void Awake()
    {

        y = transform.position.y;
        rigid = GetComponent<Rigidbody2D>();
        

    }


    void Update()
    {
        mario=GameObject.Find("Player");
        questionbox = mario.GetComponent<CharacterControllers>().ReturnMushroomQuestionBox;
        boxanim = questionbox.GetComponent<Animator>();
        if (transform.position.y >= y + 2.6f)
        {


            rigid.linearVelocity = new Vector2(0, 0);
            retur = true;
        }


        if(transform.position.y<=y &&retur)
        {

            
            questionbox.GetComponent<SpawnMushroom>().Hit = false;
            questionbox.GetComponent<SpawnMushroom>().create = false;
            boxanim.SetBool("hit", false);
            DestroyObject(gameObject);
            
        }


    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            eaten = true;
            DestroyObject(gameObject);


        }
    }
}
