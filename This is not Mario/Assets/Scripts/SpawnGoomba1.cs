using System.Collections;
using UnityEngine;


public class SpawnGoomba1 : MonoBehaviour
{

    public GameObject Goomba;
    GameObject clone1;
    Rigidbody2D rigid1;

    bool spawn=false;
    

    void OnTriggerStay2D(Collider2D obstacle)
    {
        if (obstacle.gameObject.tag == "Player" && spawn == false)
        {

            clone1 = Instantiate(Goomba, new Vector3(transform.position.x, 40, -1), Quaternion.identity);
            if (clone1.GetComponent<Rigidbody2D>() != null)
            {
                rigid1 = clone1.GetComponent<Rigidbody2D>();
                rigid1.gravityScale = 15;
            }
            else
            {
                rigid1 = clone1.AddComponent<Rigidbody2D>();
                rigid1.gravityScale = 15;
            }
            spawn = true;
            gameObject.SetActive(false);
        }

    }
}

