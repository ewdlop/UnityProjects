using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonaldTrump : MonoBehaviour {

    public GameObject walls;
    GameObject clone;
    Rigidbody2D rigid;
    public AnimationClip anim;
    

    void Awake()
    {
        
        InvokeRepeating("LaunchWalls", 0.5f/4, anim.length/4);
    }

    void LaunchWalls()
    {
        float z;
        if (walls.name == "Coins")
        {
            z = -0.5f;
        }
        else
        {
            z = 0;
        }
        clone = Instantiate(walls,new Vector3(transform.position.x- 0.6279984f, transform.position.y+0.575f,z),Quaternion.identity);
        Destroy(clone, 30f);
        if (clone.GetComponent<Rigidbody2D>()==null)
        {
            rigid=clone.AddComponent<Rigidbody2D>();
            rigid.AddForce(new Vector3(-60f + Random.value * 55f, 10f + Random.value * 10f, 0),ForceMode2D.Impulse);
            rigid.gravityScale = 3;
        }
        else
        {

            rigid = clone.GetComponent<Rigidbody2D>();
            rigid.AddForce(new Vector3(-60f + Random.value * 55f, 10f + Random.value * 10f, 0), ForceMode2D.Impulse);

        }
    }
}
