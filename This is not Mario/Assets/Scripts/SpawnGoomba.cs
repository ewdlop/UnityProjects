using System.Collections;
using UnityEngine;


public class SpawnGoomba : MonoBehaviour
{

    public GameObject Goomba;
    GameObject clone1;
    GameObject clone2;
    Rigidbody2D rigid1;
    Rigidbody2D rigid2;

    bool spawn=false;
    

    void OnTriggerStay2D(Collider2D obstacle)
    {
        if  (obstacle.gameObject.tag == "Player")
        { 
            if (spawn == false)
            {
               
                clone1 = Instantiate(Goomba, new Vector3(transform.position.x, 40, -1), Quaternion.identity);
                clone2 = Instantiate(Goomba, new Vector3(transform.position.x + 5.2f, 40, -1), Quaternion.identity);
                rigid1 = clone1.GetComponent<Rigidbody2D>();
                rigid2 = clone2.GetComponent<Rigidbody2D>();
                rigid1.gravityScale = 15;
                rigid2.gravityScale = 15;
                spawn = true;
                gameObject.SetActive(false);
                
            }
        }

    }
}

