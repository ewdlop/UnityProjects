using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl2 : MonoBehaviour {

    public GameObject player;
    public Camera cam;
    public static bool shake = false;
    float timer = 0;

    bool restore=false;
    private Vector3 n;
    private Vector3 offset;

    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

        void Start () {
      
        transform.position = new Vector3(player.transform.position.x, 3.8f + 2.6f * 4 - 1.3f, -10);
        offset = transform.position - player.transform.position;
        Debug.Log(transform.position.y);
    }
	

	void LateUpdate () {


        Vector3 moved = new Vector3(player.transform.position.x, player.transform.position.y, -10) + offset;


        if ((moved.x - cam.orthographicSize * cam.aspect) >= -60f) {

            transform.position = new Vector3(player.transform.position.x, transform.position.y, -10);


            if (shake == true)
            {
                if(restore==true)
                {
                    restore = false;
                }
                //float damp = 0.3f*Mathf.Sin(100 * timer) * Mathf.Exp(-0.1f * timer);
                float damp = 0.3f * Mathf.Sin(100 * timer);
                transform.position = new Vector3(player.transform.position.x, 3.8f + 2.6f * 4 - 1.3f + damp, -10);
                timer += Time.fixedDeltaTime;
                StartCoroutine(waitForScreamSoundFinish());
            }

            if ((moved.y + cam.orthographicSize) <= 26f && (moved.y - cam.orthographicSize) >= -0.026) {
                if (shake == true)
                {
                    //float damp = 0.3f*Mathf.Sin(100 * timer) * Mathf.Exp(-0.1f * timer);
                    float damp = 0.3f * Mathf.Sin(100 * timer);
                    transform.position = new Vector3(player.transform.position.x,3.8f + 2.6f * 4 - 1.3f + damp, -10);
                    timer += Time.fixedDeltaTime;
                    StartCoroutine(waitForScreamSoundFinish());
                }
                else
                {
                    if (restore == true)
                    {
                        transform.position = new Vector3(player.transform.position.x, 12.975f, -10);
                        restore = false;

                    }
                    transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10) + offset;
                }
            }
        }
        else
        {
            if (shake == true)
            {
                if (restore == true)
                {
                    restore = false;
                }
                //float damp = 0.3f*Mathf.Sin(100 * timer) * Mathf.Exp(-0.1f * timer);
                float damp = 0.3f * Mathf.Sin(100 * timer);
                transform.position = new Vector3(transform.position.x, 3.8f + 2.6f * 4 - 1.3f + damp, -10);
                timer += Time.fixedDeltaTime;
                StartCoroutine(waitForScreamSoundFinish());
                
            }
            else
            {
                if (restore == true)
                {
                    transform.position = new Vector3(transform.position.x, 12.975f, -10);
                    restore = false;

                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                }
            }
        }
     }
        
        //transform.position = new Vector3(player.transform.position.x,player.transform.position.y-2.6f*12,-10)+offset;
    
    IEnumerator waitForScreamSoundFinish()
    {
        //Wait Until Sound has finished playing
        if (HardBrickHitCheck.aud != null)
        {
            while (HardBrickHitCheck.aud.isPlaying)
            {
                yield return null;
            }
        }
        shake = false;
        restore= true;

    }


}
