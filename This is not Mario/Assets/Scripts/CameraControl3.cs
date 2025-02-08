using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl3 : MonoBehaviour {

    public GameObject player;
    public Camera cam;
    public static bool shake = false;
    float timer = 0;

    private Vector3 offset;

    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

        void Start () {
        transform.position = new Vector3(player.transform.position.x, 3.8f+2.6f*4-1.1f, -10);
        offset = transform.position - player.transform.position;
    }
	

	void LateUpdate () {

        if (shake == true)
        {
            
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2.6f * 4 - 1.3f + 0.3f* Mathf.Sin(100*timer)*Mathf.Exp(-0.1f*timer), -10);
            timer += Time.fixedDeltaTime;
            StartCoroutine(waitForScreamSoundFinish());
        }
        else
        {
            Vector3 moved = new Vector3(player.transform.position.x, player.transform.position.y, -10) + offset;
            if ((moved.y + cam.orthographicSize) <= 25.9f&& (moved.y - cam.orthographicSize)>=0)
            {

                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10) + offset;
                //transform.position = new Vector3(player.transform.position.x,player.transform.position.y-2.6f*12,-10)+offset;


            }
            else
            {

                transform.position = new Vector3(player.transform.position.x, transform.position.y, -10);
            }
        }
        
        //transform.position = new Vector3(player.transform.position.x,player.transform.position.y-2.6f*12,-10)+offset;
    }
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
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2.6f * 4 - 1.3f + 0.3f, -10);
    }


}
