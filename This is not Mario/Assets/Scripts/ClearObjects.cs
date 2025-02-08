using UnityEngine;

public class ClearObjects : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag!="Player")
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            Destroy(other.gameObject);
        }
    }
}
