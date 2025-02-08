using UnityEngine;

public class BounceTileA : MonoBehaviour
{
    public bool touched;
    public bool bounced;
    public GameObject bounceGameObject;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!touched && collision.gameObject.tag == "Player")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(50f, 0);
            touched = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!bounced && collision.gameObject == bounceGameObject)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-50f, 0);
            bounced = true;
        }
    }
}
