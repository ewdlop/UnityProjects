using UnityEngine;

public class HardBrickHitCheck : MonoBehaviour {

    bool Hitten = false;
    public bool fatal;

    public Transform Hitcheck;
    public float Hitradius;
    public LayerMask Playerlayer;
   
    public GameObject mario;

    public Camera cam;
    Rigidbody2D rigid;
    Rigidbody2D mariorigid;
    BoxCollider2D col;
    Animator anim;
    AudioSource MarioDies;
    AudioSource Backgroundmusic;
    public static AudioSource aud;

    void Start()
    {
        rigid = mario.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Hitten = Physics2D.OverlapCircle(Hitcheck.position, Hitradius, Playerlayer);
        if (Hitten)
        {
            
            aud = GetComponent<AudioSource>();
            aud.Play();
            if (!fatal)
            {
                CameraControl2.shake = true;
                rigid.linearVelocity = new Vector2(0, 0);
                rigid.AddForce(new Vector2(0, -80f));
            }
            else
            {
                mariorigid = mario.GetComponent<Rigidbody2D>();
                anim = mario.GetComponent<Animator>();
                MarioDies = mario.GetComponent<AudioSource>();
                Backgroundmusic = cam.GetComponent<AudioSource>();
                col = mario.GetComponent<BoxCollider2D>();
                Backgroundmusic.Stop();
                col.enabled = false;
                CharacterControllers.isDead = true;
                anim.SetTrigger("Die");
                mariorigid.linearVelocity = new Vector2(0, 0);
                mariorigid.gravityScale = 4;
                mariorigid.AddForce(new Vector2(0, 1500));
                MarioDies.Play();

                GameControl.instance.MarioDied();
            }
            }

        }
    }


