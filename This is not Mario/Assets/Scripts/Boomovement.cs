using UnityEngine;

public class Boomovement : MonoBehaviour
{
    public GameObject mario;
    public bool realone;
    public bool reversed;


    bool clone2;
    GameObject clone;
    Animator anim;
    Rigidbody2D rigid;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ((mario.transform.position.x - transform.position.x) > 0)
        {
            if (Mathf.Sign(transform.localScale.x) < 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            if (Mathf.Sign(transform.localScale.x) + Mathf.Sign(mario.transform.localScale.x) == 2)
            {
                if (reversed)
                {
                    anim.SetBool("hide", true);
                    rigid.velocity = new Vector3(0, 0, 0);
                    //if (clone2 && realone)
                    //{
                    //    clone = Instantiate(gameObject, new Vector3(transform.position.x + 5, transform.position.y + 5, -1), Quaternion.identity);
                    //    clone.GetComponent<Boomovement>().realone = false;
                    //}
                    //clone2 = false;
                }
                else
                {
                    rigid.velocity = new Vector3((mario.transform.position.x - transform.position.x) * Time.deltaTime * 10f, (mario.transform.position.y - transform.position.y) * Time.deltaTime * 10f, 0);
                    anim.SetBool("hide", false);
                    //clone2 = true;
                    transform.localScale = new Vector3(1.002f * transform.localScale.x, 1.002f * transform.localScale.y, transform.localScale.z);
                }
            }
            else if (reversed)
            {
                rigid.velocity = new Vector3((mario.transform.position.x - transform.position.x) * Time.deltaTime * 10f, (mario.transform.position.y - transform.position.y) * Time.deltaTime * 10f, 0);
                anim.SetBool("hide", false);
                //clone2 = true;
                transform.localScale = new Vector3(1.002f * transform.localScale.x, 1.002f * transform.localScale.y, transform.localScale.z);
            }
            else
            {
                anim.SetBool("hide", true);
                rigid.velocity = new Vector3(0, 0, 0);
                //if (clone2 && realone)
                //{
                //    clone = Instantiate(gameObject, new Vector3(transform.position.x + 5, transform.position.y + 5, -1), Quaternion.identity);
                //    clone.GetComponent<Boomovement>().realone = false;
                //}
                //clone2 = false;
            }
        }
        else if ((mario.transform.position.x - transform.position.x) < 0)
        {
            if (Mathf.Sign(transform.localScale.x) > 0)
            {
                transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            if (Mathf.Sign(transform.localScale.x) + Mathf.Sign(mario.transform.localScale.x) == -2)
            {
                if (reversed)
                {
                    anim.SetBool("hide", true);
                    rigid.velocity = new Vector3(0, 0, 0);
                    //if (clone2 && realone)
                    //{
                    //    clone = Instantiate(gameObject, new Vector3(transform.position.x + 5, transform.position.y + 5, -1), Quaternion.identity);
                    //    clone.GetComponent<Boomovement>().realone = false;
                    //    clone2 = false;
                    //}
                }
                else
                {
                    rigid.velocity = new Vector3((mario.transform.position.x - transform.position.x) * Time.deltaTime * 10f, (mario.transform.position.y - transform.position.y) * Time.deltaTime * 10f, 0);
                    anim.SetBool("hide", false);
                    //clone2 = true;
                    transform.localScale = new Vector3(1.002f * transform.localScale.x, 1.002f * transform.localScale.y, transform.localScale.z);
                }
            }
            else if (reversed)
            {
                rigid.velocity = new Vector3((mario.transform.position.x - transform.position.x) * Time.deltaTime * 10f, (mario.transform.position.y - transform.position.y) * Time.deltaTime * 10f, 0);
                anim.SetBool("hide", false);
                //clone2 = true;
                transform.localScale = new Vector3(1.002f * transform.localScale.x, 1.002f * transform.localScale.y, transform.localScale.z);
            }
            else
            {
                anim.SetBool("hide", true);
                rigid.velocity = new Vector3(0, 0, 0);
                //if (clone2 && realone)
                //{
                //    clone = Instantiate(gameObject, new Vector3(transform.position.x + 5, transform.position.y + 5, -1), Quaternion.identity);
                //    clone.GetComponent<Boomovement>().realone = false;
                //    clone2 = false;
            }
        }
    }
}
