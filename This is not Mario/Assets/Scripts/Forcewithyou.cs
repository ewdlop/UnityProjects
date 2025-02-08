using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcewithyou : MonoBehaviour {


    float x;
    float y;
    Rigidbody2D rigid;
    BoxCollider2D [] box;
    Animator anim;
    public GameObject forcewithyou;
    public bool triggered=false;



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

            rigid.velocity = new Vector2(0, 0);
        }

        /**if (Johncena == true)
        {
            Vector2 resize= gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
            mariobox.size = resize;
        }**/

        
    }

    void OnTriggerStay2D(Collider2D collision )
    {
        if(collision.gameObject.tag=="Player"&&!triggered){

            Instantiate(forcewithyou, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
            triggered = true;
            DestroyObject(gameObject);
            CharacterControllers.starmode = true;

        }
    }
}
