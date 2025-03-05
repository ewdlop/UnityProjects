using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagpoleMove : MonoBehaviour {

    public GameObject flagpole;
    public GameObject trigger;
    public GameObject flag;
    public static bool rotate = false;
    Rigidbody2D rigid;

    private void Update()
    {
        if(flagpole!=null&&rotate==true)
        {
            flagpole.transform.Rotate(new Vector3(0, 0, 10f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Shell")
        {
            rigid = flagpole.GetComponent<Rigidbody2D>();
            rigid.linearVelocity = new Vector3(40f, 0, 0);
            
            rigid.bodyType = RigidbodyType2D.Dynamic;
            flag.GetComponent<Rigidbody2D>().bodyType= RigidbodyType2D.Kinematic;
            rotate = true;
            Destroy(trigger);
        }
    }
}
