using UnityEngine;

public class FinalPlatform : MonoBehaviour {

    Rigidbody2D rigid;
    public float speed;

	// Use this for initialization
	void Start () {


        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector3(speed, 0, 0);
    }
	
	
	void Update () {
		if(transform.position.x> -25.08f)
        {

            transform.position = new Vector3(-25.08f, transform.position.y,0);
            rigid.velocity = new Vector3(-1*speed*10, 0, 0);



        }


                

	}
}
