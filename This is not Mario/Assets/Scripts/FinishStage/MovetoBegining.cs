using UnityEngine;

public class MovetoBegining : MonoBehaviour {


    public GameObject platform;
    public GameObject boo;
    bool spawn = false;
    



    void OnTriggerStay2D(Collider2D obstacle)
    {
        if (obstacle.gameObject.tag == "Player")
        {
            if (spawn == false)
            {


                spawn = true;
                gameObject.SetActive(false);
                boo.SetActive(false);
                obstacle.gameObject.transform.position = new Vector3(-21.1599f, 25f, -1);
                platform.transform.position = new Vector3(-26f, platform.transform.position.y,0);
                obstacle.gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
                obstacle.gameObject.GetComponent<CharacterControllers>().maxSpeed = 20f;
                obstacle.gameObject.GetComponent<CharacterControllers>().jumpforce = 25f;
                foreach (BoxCollider2D boxCollider2D in obstacle.gameObject.GetComponents<BoxCollider2D>())
                {
                    if (boxCollider2D.isTrigger)
                    {
                        Destroy(boxCollider2D);
                        BoxCollider2D newCollider = obstacle.gameObject.AddComponent<BoxCollider2D>();
                        newCollider.isTrigger = true;
                    }
                    else
                    {
                        obstacle.gameObject.AddComponent<BoxCollider2D>();
                    }
                }
                Rigidbody2D rigid = platform.GetComponent<Rigidbody2D>();
                rigid.linearVelocity = new Vector3(Mathf.Abs(rigid.linearVelocity.x), 0, 0);
            }
        }
    }
}
