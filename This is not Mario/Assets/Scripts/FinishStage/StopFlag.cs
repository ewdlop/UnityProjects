using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StopFlag : MonoBehaviour {

    float y;
    bool landed = false;
    bool moved = false;
    Rigidbody2D rigid;
    Rigidbody2D mariorigid;
    Animator anim;
    AudioSource stageclear;

    public GameObject mario;
    public GameObject Loading;
    public GameObject Blackscreen;
  
    

    void Update () {
   

        if (transform.position.y <= 6.3f)
        {
            rigid=GetComponent<Rigidbody2D>();
            rigid.gravityScale = 0;
            rigid.linearVelocity = new Vector2(0, 0);
            landed = true;
        }


        if(landed&&!moved&&!FlagpoleMove.rotate)
        {
            moved = true;
            mariorigid = mario.GetComponent<Rigidbody2D>();
            anim = mario.GetComponent<Animator>();
            if (TouchingFlagPole.goingdown != null)
            {
                TouchingFlagPole.goingdown.Stop();
            }
            StartCoroutine(waitForGoingDownSound());
            stageclear = GetComponent<AudioSource>();
            stageclear.Play();
            StartCoroutine(waitForStageCompleteSound());


        }

	}


    IEnumerator waitForGoingDownSound()
    {
        if (TouchingFlagPole.goingdown != null)
        {
            //Wait Until Sound has finished playing
            while (TouchingFlagPole.goingdown.isPlaying)
            {
                yield return null;
            }
        }

        //Auidio has finished playing
        //Debug.Log(GameControl.stage);

        Vector3 thescale = mario.transform.localScale;

        if (GameControl.stage == 0)
        {
            //anim.SetTrigger("Land");
            //if (Mathf.Sign(thescale.x) > 0)
            //{
            //    thescale.x *= -1;
            //    mario.transform.localScale = thescale;
            //}
            //mario.transform.position = new Vector3(mario.transform.position.x - 1.3f, 3.9f, -1);
            ////mariorigid.velocity = new Vector2(-25f, 0);
        }
        else if (GameControl.stage == 1)
        {
            anim.SetFloat("Speed", 1);
            mario.transform.localScale = new Vector3(Mathf.Abs(mario.transform.localScale.x), mario.transform.localScale.y, mario.transform.localScale.z);
            mario.transform.position = new Vector3(mario.transform.position.x + 2.6f, 3.9f, -1);
            mariorigid.linearVelocity = new Vector2(25f, 0);
        }
        else
        if (GameControl.stage == 2)
        {

        }
    }
    IEnumerator waitForStageCompleteSound()
    {
        //Wait Until Sound has finished playing
        if (stageclear != null)
        {
            while (stageclear.isPlaying)
            {
                yield return null;
            }
        }

        //load stage1
        if (GameControl.stage == 0)
        {
            //GameControl.instance.Resetbool();
            GameControl.highscore = GameControl.coincount;

            GameObject control = GameObject.Find("Control");
            if (control.GetComponent<Control>().hellmode)
            {
                SceneManager.LoadScene("Stage1HellMode", LoadSceneMode.Single);
            }
            else if (control.GetComponent<Control>().nightmaremode)
            {
                SceneManager.LoadScene("Stage1NightMareMode", LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene("Stage1", LoadSceneMode.Single);
            }
            Loading.SetActive(true);
            Blackscreen.SetActive(true);
            
            GameControl.saveindex = 0;
            GameControl.stage = 1;
        }
        else if (GameControl.stage == 1)
        {
            //GameControl.instance.Resetbool();
            GameControl.highscore = GameControl.coincount;
            GameObject control = GameObject.Find("Control");
            if (control.GetComponent<Control>().hellmode)
            {
                SceneManager.LoadScene("Stage2HellMode", LoadSceneMode.Single);
            }
            else if (control.GetComponent<Control>().nightmaremode)
            {
                SceneManager.LoadScene("Stage2NightMareMode", LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene("Stage2", LoadSceneMode.Single);
            }
            Loading.SetActive(true);
            Blackscreen.SetActive(true);
            GameControl.saveindex = 0;
            GameControl.stage = 2;
        }
        else if (GameControl.stage == 2)
        {
            Blackscreen.SetActive(true);
            Loading.SetActive(true);
            SceneManager.LoadScene("Thanks", LoadSceneMode.Single);
            GameControl.saveindex = 0;
            GameControl.stage = 3;
        }
    }
}
