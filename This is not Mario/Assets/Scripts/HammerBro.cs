using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBro : MonoBehaviour {

    public GameObject hammer;
    GameObject clone;
    public GameObject mario;
    Rigidbody2D rigid;
    public AnimationClip anim;
    Animator hammeranim;
    public GameObject goomba;

    void Awake()
    {
        InvokeRepeating("CreateHammer", 0, anim.length);
        InvokeRepeating("LaunchHammer", 1f, anim.length);
        transform.parent = null;

    }

    void Update()
    {
        transform.localScale = new Vector3(-1*Mathf.Sign(mario.transform.position.x - transform.position.x)*16, 16, transform.localScale.z);
        if (goomba != null)
        {


            transform.position = new Vector3(goomba.transform.position.x - 0.4f * transform.localScale.x / 16, goomba.transform.position.y + 3.2f, 0);

        }
        }

    void CreateHammer()
    {

        clone = Instantiate(hammer, new Vector3(transform.position.x + 0.003124237f* Mathf.Sign(transform.localScale.x), transform.position.y + 1f, -1), Quaternion.identity);
        clone.transform.parent = gameObject.transform;
        if (clone.GetComponent<Rigidbody2D>() == null)
        {
            rigid = clone.AddComponent<Rigidbody2D>();
        }
        else
        {

            rigid = clone.GetComponent<Rigidbody2D>();

        }
        
    }

    void LaunchHammer()
    {

        //clone = Instantiate(hammer,new Vector3(transform.position.x- 0.003124237f, transform.position.y+0.145f, -1),Quaternion.identity);
        hammeranim = clone.GetComponent<Animator>();
        hammeranim.SetTrigger("Spin");
        clone.transform.localScale=new Vector3(Mathf.Sign(transform.localScale.x),1,clone.transform.localScale.z);
        
        rigid.bodyType=RigidbodyType2D.Dynamic;
        if (goomba != null)
        {
            rigid.AddForce(new Vector3(Mathf.Sign(mario.transform.position.x - transform.position.x) *10f + Random.value * 2, 5f + Random.value * 5f, 0), ForceMode2D.Impulse);
        }
        else
        {
            DestroyObject(gameObject);
            DestroyObject(clone);
        }
            rigid.transform.parent = null;
            
    }


}
