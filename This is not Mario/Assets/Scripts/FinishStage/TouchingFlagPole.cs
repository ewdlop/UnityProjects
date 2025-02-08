
using UnityEngine;

public class TouchingFlagPole : MonoBehaviour
{


    public GameObject flag;
    Rigidbody2D flagRigid;
    Rigidbody2D MarioRigid;
    public static bool triggered = false;
    public static bool touched = false;
    Animator anim;
    public Camera cam;
    public static AudioSource goingdown;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player") && !triggered && Launch.flyingoff == false)
        {
            triggered = true;
            touched = true;
            MarioRigid = collision.gameObject.GetComponent<Rigidbody2D>();
            anim = collision.gameObject.GetComponent<Animator>();
            flagRigid = flag.GetComponent<Rigidbody2D>();
            flag.transform.position = new Vector2(flag.transform.position.x, collision.transform.position.y);
            anim.SetFloat("Speed", 0);
            anim.SetBool("GrabPole", true);
            MarioRigid.velocity = new Vector2(0, 0);
            flagRigid.gravityScale = 14;
            GameObject control = GameObject.Find("Control");
            if (GameControl.stage != 2)
            {
                collision.gameObject.transform.position = new Vector3(218.5f, collision.transform.position.y, -1);
            }
            if (!control.GetComponent<Control>().hellmode)
            {
                if (GameControl.stage == 0)
                {
                    GameControl.BeatStage0 = true;
                    if (GameControl.BeatStage1 == true)
                    {
                        GameControl.BeatStage1 = true;
                    }
                    else
                    {
                        GameControl.BeatStage1 = false;
                    }
                    if (GameControl.BeatStage2 == true)
                    {
                        GameControl.BeatStage2 = true;
                    }
                    else
                    {
                        GameControl.BeatStage2 = false;
                    }
                }
                if (GameControl.stage == 1)
                {
                    GameControl.BeatStage0 = true;
                    GameControl.BeatStage1 = true;
                    if (GameControl.BeatStage2 == true)
                    {
                        GameControl.BeatStage2 = true;
                    }
                    else
                    {
                        GameControl.BeatStage2 = false;
                    }

                }



                if (GameControl.stage == 2)
                {
                    GameControl.BeatStage0 = true;
                    GameControl.BeatStage1 = true;
                    GameControl.BeatStage2 = true;
                }
            }
           
            if (!control.GetComponent<Control>().hellmode)
                GameControl.instance.Save();
            GameControl.saveindex = 0;
            cam.GetComponent<AudioSource>().Stop();
            goingdown = GetComponent<AudioSource>();
            goingdown.Play();
            if (GameControl.stage == 1)
            {

                GameObject stopper = GameObject.Find("Stopper");
                GameObject stopper2 = GameObject.Find("Stopper2");
                stopper.GetComponent<BoxCollider2D>().enabled = true;
                stopper2.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

    }

}

    