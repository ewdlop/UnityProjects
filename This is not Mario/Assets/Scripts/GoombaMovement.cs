using System.Collections;
using UnityEngine;

public class GoombaMovement : MonoBehaviour
{

    bool grounded;
    float turnaround = 1;
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask whatIsground;
    Rigidbody2D rigid;
    public bool reversed = false;
    public bool notmirror = false;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        if (reversed)
        {
            gameObject.transform.localScale = new Vector3(-1 * gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsground);
        if (grounded)
        {
            if (reversed)
            {
                if (gameObject.name == "GG" || gameObject.name == "BonucingShell")
                {
                    if (gameObject.name == "GG")
                    {
                        rigid.velocity = new Vector2(37 * turnaround, 0);
                    }

                else {
                    rigid.velocity = new Vector2(50 * turnaround, 0);
                }

                }
                else {
                    rigid.velocity = new Vector2(10 * turnaround, 0);
                }
            }
            else
            {
                if (gameObject.name == "GG" || gameObject.name == "BonucingShell")
                {
                    if (gameObject.name == "GG")
                    {
                        rigid.velocity = new Vector2(40* turnaround, 0);
                    }

                    else
                    {
                        rigid.velocity = new Vector2(-50 * turnaround, 0);
                    }
                }
                else
                {
                    rigid.velocity = new Vector2(-10 * turnaround, 0);
                }
            }

        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Onfloor")
        {
            turnaround = turnaround*-1;
            rigid.velocity = new Vector2(50 * turnaround, 0);
            if (!notmirror)
            gameObject.transform.localScale = new Vector3(-1 *gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
    }

}