using UnityEngine;

public class SpawnGiantMissileBills : MonoBehaviour {

    public GameObject GiantMissileBills;
    GameObject clone;
    Rigidbody2D rigid;
    bool spawn=false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!spawn && collision.gameObject.tag == "Player")
        {
            clone = Instantiate(GiantMissileBills, new Vector3(transform.position.x + 20f, transform.position.y, -3), Quaternion.identity);
            rigid = clone.GetComponent<Rigidbody2D>();
            rigid.linearVelocity = new Vector2(-100, 0);
            spawn = true;
            gameObject.SetActive(false);
        }
    }
}
