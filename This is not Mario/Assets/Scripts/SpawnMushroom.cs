using UnityEngine;

public class SpawnMushroom : MonoBehaviour
{


    public Transform Hitcheck;
    public float Hitradius;
    float height;
    public LayerMask Playerlayer;
    bool Hitten = false;
    public bool Hit = false;
    public bool create = false;

    Animator anim;
    Rigidbody2D rigid;
    Rigidbody2D mariorigid;
    Rigidbody2D mushroomrigid;


    public GameObject mario;
    public GameObject mushroom;
    public GameObject poisomushroom;
    public GameObject star;
    public GameObject shrinkmushroom;
    public GameObject returnmushroom;
    public GameObject dancemushroom;
    public GameObject wallmushroom;
    GameObject mush;
    AudioSource tada;
    GameObject nope;
    public bool spawnstar;
    public bool poisonous;
    public bool shrink;
    public bool retur;
    public bool dance;
    public bool wall;

    void Start()

    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        mariorigid = mario.GetComponent<Rigidbody2D>();
        height = transform.position.y;

    }


    void Update()
    {

        if (Johncenamove.eaten == false)
        {
            if (transform.position.y < height)
            {
                rigid.linearVelocity = new Vector2(0, 0);
                rigid.gravityScale = 0;
                transform.position = new Vector2(transform.position.x, height);
            }

            else
            {
                if (transform.position.y >= height + 1.3f)
                {
                    rigid.linearVelocity = new Vector2(0, 0);
                    rigid.gravityScale = 1.2f;
                }
                else
                {
                    if (transform.position.y == height && Hit && create == false)
                    {
                        rigid.linearVelocity = new Vector2(0, 0);
                        rigid.gravityScale = 0;
                        if (poisonous)
                        {
                            mush = Instantiate(poisomushroom, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
                        }
                        else
                        if (spawnstar)
                        {
                            mush = Instantiate(star, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
                        }
                        else
                        {
                            if (shrink)
                            {

                                mush = Instantiate(shrinkmushroom, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);

                            }
                            else
                            {
                                if (retur)
                                {
                                    mush = Instantiate(returnmushroom, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
                                    nope = GameObject.Find("Nope");
                                    nope.GetComponent<AudioSource>().Play();
                                }


                                else
                                {
                                    if (dance)
                                    {
                                        mush = Instantiate(dancemushroom, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
                                    }
                                        else
                                    {
                                        if (wall)
                                        {
                                            mush = Instantiate(wallmushroom, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
                                        }
                                        else
                                        mush = Instantiate(mushroom, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
                                    }
                                
                                    
                                }
                            }

                        }
                        mushroomrigid = mush.GetComponent<Rigidbody2D>();
                        mushroomrigid.AddForce(new Vector2(0, 380f));
                        create = true;
                        tada = GetComponent<AudioSource>();
                        tada.Play();
                    }
                }
            }
            Hitten = Physics2D.OverlapCircle(Hitcheck.position, Hitradius, Playerlayer);
            if (Hitten && !Hit)
            {

                Hit = true;
                anim.SetBool("hit", true);
                rigid.gravityScale = 1;
                mariorigid.linearVelocity = new Vector2(0, 0);
                mariorigid.AddForce(new Vector2(0, -500f));
                mario.GetComponent<CharacterControllers>().ReturnMushroomQuestionBox = gameObject;

            }
        }
    }
}


