
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Launch : MonoBehaviour
{


    Rigidbody2D MarioRigid;
    public static bool flyingoff=false;
    public static bool triggered=false;
    BoxCollider2D box;
    public GameObject brickbox;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == ("Player")&& !triggered)
        {
            if (TouchingFlagPole.touched == false)
            {
                flyingoff = true;
                triggered = true;
                MarioRigid = collision.gameObject.GetComponent<Rigidbody2D>();
                box = brickbox.GetComponent<BoxCollider2D>();

                MarioRigid.linearVelocity = new Vector3(100f, 0, 0);

                box.enabled = false;
            }
   

                

            
        }

    }
}
