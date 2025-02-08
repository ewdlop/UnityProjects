using UnityEngine;
using UnityEngine.UI;

public class CharacterControllers : MonoBehaviour
{
    bool facingRight = true;
    bool grounded;

    public Transform groundCheck;
    public float maxSpeed;
    public float groundRadius;
    public float jumpforce;
    public LayerMask whatIsground;
    public AudioSource jump;
    public AudioSource bigjump;
    public AudioSource coin;
    public Text countText;
    public Text highScoreText;
    public Text deathcountText;
    public AudioClip clip;
    public Camera cam;
    public GameObject GiantMissileBills;
    public static bool isDead;
    public static bool starmode;
    public GameObject ReturnMushroomQuestionBox;

    Animator anim;
    Rigidbody2D rigid;
    BoxCollider2D col;
    AudioSource MarioDies;
    AudioSource Backgroundmusic;
    GameObject[] coins;
    GameObject[] killers;
    GameObject[] floors;
    GameObject platform;
    float platformoffsetX;
    float finalplatformoffsetX;
    float move;

    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        MarioDies = GetComponent<AudioSource>();
        Backgroundmusic = cam.GetComponent<AudioSource>();
    }


    void Update()
    {
        GameObject control = GameObject.Find("Control");
        if (control.GetComponent<Control>().heavenmode)
        {
            highScoreText.text = "HighScore: " + GameControl.highdistance.ToString();
            if (!isDead)
                deathcountText.text = "Coin: " + GameControl.coincount.ToString();
        }
        else
        {
            highScoreText.text = "High Score: " + GameControl.highscore.ToString();
            deathcountText.text = "Death: " + GameControl.deathcount.ToString();
            countText.text = "Coin: " + GameControl.coincount;

        }
        //HeavenMode
        /**if (control.GetComponent<Control>().heavenmode&&transform.position.y>=134.32f)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }**/

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsground);
        if (!control.GetComponent<Control>().heavenmode)
        {
            move = Input.GetAxis("Horizontal");
        }
        else
        {
            move = 0;
        }

        if (!isDead && !TouchingFlagPole.touched && !Launch.flyingoff && !Stuckonelevator.stuck)
        {
            if (control.GetComponent<Control>().heavenmode)
            {
                rigid.velocity = new Vector2(rigid.velocity.x * Mathf.Abs(transform.localScale.x / 16), rigid.velocity.y);
            }
            else
            {
                rigid.velocity = new Vector2(move * maxSpeed * Mathf.Abs(transform.localScale.x / 16), rigid.velocity.y);
            }
            if (move > 0 && !facingRight && grounded)
            {
                Flip();
            }
            else if (move < 0 && facingRight && grounded)
            {
                Flip();
            }

            if (grounded)
            {
                anim.SetFloat("Speed", 0);
                anim.SetTrigger("Land");

                //normal jump
                if (Input.GetKeyDown(KeyCode.Space) && move == 0)
                {
                    anim.SetFloat("Speed", 0);
                    anim.SetTrigger("Jump");
                    jump.Play();

                    if (control.GetComponent<Control>().heavenmode)
                    {

                        rigid.AddForce(new Vector2(0, 25f), ForceMode2D.Impulse);
                    }
                    else
                    {
                        rigid.velocity = new Vector2(move * maxSpeed * Mathf.Abs(transform.localScale.x / 16), rigid.velocity.y);
                        rigid.velocity = new Vector2(rigid.velocity.x, jumpforce * Mathf.Abs(transform.localScale.y / 16));
                    }
                }
            }
            else
            {
                anim.SetTrigger("Jump");
            }
            if (grounded && !control.GetComponent<Control>().heavenmode)
            {
                //Normal moving
                anim.SetFloat("Speed", Mathf.Abs(move));
                rigid.velocity = new Vector2(move * maxSpeed * Mathf.Abs(transform.localScale.x / 16), rigid.velocity.y);

                if (Input.GetKeyDown(KeyCode.Space) && move != 0)
                {
                    //Moving then Jump
                    anim.SetFloat("Speed", 0);
                    anim.SetTrigger("Jump");
                    //rigid.AddForce(new Vector2(0, jumpforce));
                    rigid.velocity = new Vector2(rigid.velocity.x, jumpforce * Mathf.Abs(transform.localScale.y / 16));
                    //rigid.AddForce(new Vector2(0, 25f), ForceMode2D.Impulse);
                    bigjump.Play();

                }
            }
        }
        //Starmode

        if (starmode)
        {
            coins = GameObject.FindGameObjectsWithTag("Coin");
            foreach (GameObject obj in coins)
            {
                Vector3 difference = new Vector3(transform.position.x - obj.transform.position.x,
                                                 transform.position.y - obj.transform.position.y,
                                                 0);
                float angle = Mathf.Atan2(transform.position.y - obj.transform.position.y, transform.position.x - obj.transform.position.x) * 180 / Mathf.PI;
                obj.transform.position += difference * Time.deltaTime * 1.5f;
                obj.transform.eulerAngles = new Vector3(0, 0, angle);
            }
            killers = GameObject.FindGameObjectsWithTag("Killer");
            foreach (GameObject obj in killers)
            {
                Vector3 difference = new Vector3(transform.position.x - obj.transform.position.x,
                                                 transform.position.y - obj.transform.position.y,
                                                 0);
                obj.transform.position += difference * Time.deltaTime * 1.5f;
                float angle2 = Mathf.Atan2(transform.position.y - obj.transform.position.y, transform.position.x - obj.transform.position.x) * 180 / Mathf.PI - 90;
                obj.transform.eulerAngles = new Vector3(0, 0, angle2);
            }
            floors = GameObject.FindGameObjectsWithTag("Floor");
            foreach (GameObject obj in floors)
            {
                Vector3 difference = new Vector3(transform.position.x - obj.transform.position.x, transform.position.y - obj.transform.position.y, 0);
                obj.transform.position += difference * Time.deltaTime * 1.5f;
                float angle3 = Mathf.Atan2(transform.position.y - obj.transform.position.y, transform.position.x - obj.transform.position.x) * 180 / Mathf.PI;
                obj.transform.eulerAngles = new Vector3(0, 0, angle3);
            }
        }
        if (platform != null)
        {
            platformoffsetX = platform.transform.position.x - finalplatformoffsetX;
            finalplatformoffsetX = platform.transform.position.x;
            transform.position = new Vector2(transform.position.x + platformoffsetX, transform.position.y);
        }
    }

    void Flip()
    {
        anim.SetTrigger("Flip");
        facingRight = !facingRight;
        Vector3 thescale = transform.localScale;
        thescale.x *= -1;
        transform.localScale = thescale;
    }
    void OnTriggerEnter2D(Collider2D obstacle)
    {
        if (obstacle.gameObject.tag == "Killer")
        {
            Backgroundmusic.Stop();
            //obstacle.gameObject.SetActive(false);
            col.enabled = false;
            isDead = true;
            anim.SetTrigger("Die");
            rigid.velocity = new Vector2(0, 0);
            rigid.gravityScale = 4;
            rigid.AddForce(new Vector2(0, 1500));
            MarioDies.Play();
            GameControl.instance.MarioDied();
        }
        if (obstacle.gameObject.tag == "Falling")
        {
            Backgroundmusic.Stop();
            col.enabled = false;
            isDead = true;
            anim.SetTrigger("Die");
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(new Vector2(0, 2000));
            MarioDies.Play();
            GameControl.instance.MarioDied();
        }
        if (obstacle.gameObject.tag == "Coin")
        {
            if (gameObject.tag == "JohnCena")
            {
                obstacle.GetComponent<CircleCollider2D>().isTrigger = false;
            }
            else
            {
                obstacle.gameObject.SetActive(false);
                GameControl.coincount++;
                coin.Play();

            }
        }
        if (obstacle.gameObject.tag == "MissileBills")
        {
            Backgroundmusic.Stop();
            col.enabled = false;
            isDead = true;
            anim.SetTrigger("Die");
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(new Vector2(0, 1500));
            MarioDies.Play();
            GameControl.instance.MarioDied();
        }
        if (obstacle.gameObject.tag == "Platform")
        {
            platform = obstacle.gameObject;
            finalplatformoffsetX = platform.transform.position.x;
        }
        if (obstacle.gameObject.tag == "BouncedBrick" && obstacle.gameObject.GetComponent<BounceTileA>().bounced)
        {
            Backgroundmusic.Stop();
            col.enabled = false;
            isDead = true;
            anim.SetTrigger("Die");
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(new Vector2(0, 1500));
            MarioDies.Play();
            GameControl.instance.MarioDied();
        }
    }
    void OnCollisionStay2D(Collision2D obstacle)
    {
        if (obstacle.gameObject.tag == "Brick")
        {
            Backgroundmusic.Stop();
            obstacle.gameObject.SetActive(false);
            col.enabled = false;
            isDead = true;
            anim.SetTrigger("Die");
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(new Vector2(0, 1600));
            MarioDies.Play();

            GameControl.instance.MarioDied();

        }

        if (obstacle.gameObject.tag == "Cloud")
        {

            rigid.AddForce(new Vector2(0, 100f));

        }
    }
    void OnCollisionEnter2D(Collision2D obstacle)
    {

        GameObject control = GameObject.Find("Control");
        {
            if (obstacle.gameObject.tag == "Coin" && control.GetComponent<Control>().heavenmode)
            {
                obstacle.gameObject.SetActive(false);
                GameControl.coincount++;
                coin.Play();
            }
        }
    }

    void OnTriggerStay2D(Collider2D obstacle)
    {
        if (obstacle.gameObject.tag == "Platform")
        {
            platform = obstacle.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D obstacle)
    {
        if (obstacle.gameObject.tag == "Platform")
        {
            platform = null;
        }
    }
}
