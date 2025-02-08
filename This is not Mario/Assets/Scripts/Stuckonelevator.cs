using UnityEngine;

public class Stuckonelevator : MonoBehaviour {

    public static bool stuck=false;

    public GameObject mario;
    Rigidbody2D mariorigid;
    Rigidbody2D rigid;
    Animator anim;
    public bool sticky = false;
    public bool stage2 = false;
    float height;
    float x;
    float y;

    private void Awake()
    {

        x = transform.position.x;
        y = transform.position.y;

        transform.position = new Vector3(x, y, transform.position.z);


    }



    void Update()
    {
        if (stage2)
        {
            height = 42;
        }
        else
        {
            height = 38;
        }
        if (transform.position.y>=height)
        {
            transform.position = new Vector3(transform.position.x, -6f, transform.position.z);
        }
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector3(0, 10f, 0);
    }

   
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"&&sticky)
        {
            stuck = true;
            anim = mario.GetComponent<Animator>();
            mariorigid = mario.GetComponent<Rigidbody2D>();
            anim.SetFloat("Speed", 0);
            anim.SetTrigger("Land");
            
            mariorigid.velocity = new Vector3(0, 0, 0);
        }
    }
}
