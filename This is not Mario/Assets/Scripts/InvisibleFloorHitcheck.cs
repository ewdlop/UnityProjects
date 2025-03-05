using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleFloorHitcheck : MonoBehaviour {

    public Transform Hitcheck;
    public float Hitradius;
    public LayerMask Playerlayer;
    bool Hitten=false;
    bool Hit = false;
    public GameObject mario;

    Rigidbody2D rigid;
    SpriteRenderer sprite;
    BoxCollider2D col;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        rigid = mario.GetComponent<Rigidbody2D>();
    }

    

	void Update () {
		Hitten= Physics2D.OverlapCircle(Hitcheck.position, Hitradius, Playerlayer);
        if(Hitten&&!Hit)
        {
            sprite.enabled = true;
            col.enabled = true;
            Hit = true;
            rigid.linearVelocity = new Vector2(0, 0);
            rigid.AddForce(new Vector2(0,-3000f));
        }
    }
}
