using UnityEngine;

public class ElevatorMode : MonoBehaviour {

    Rigidbody2D rigid;
    Rigidbody2D rigid2;

    bool step ;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !step)
        {
            rigid = gameObject.AddComponent<Rigidbody2D>();
            rigid2 = collision.gameObject.GetComponent<Rigidbody2D>();
            step = true;
            rigid.AddForce(new Vector2(0, 5000f));
            rigid2.linearVelocity=new Vector2(0f, 0f);
            rigid2.AddForce(new Vector2(0, 5000f));


        }
    }
}


