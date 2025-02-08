using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlNigtmareMode1 : MonoBehaviour
{

    public GameObject player;

    public Camera cam;

    private Vector3 offset;
    GameObject countdown;


    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {

        //Vector3 update=new Vector3(player.transform.position.x, 3.8f + 2.6f * 4 - 1.3f, -10);
        //offset = update - player.transform.position;
        offset = new Vector3(0, 0, 0);
    }


    void LateUpdate()
    {
       

        Vector3 moved = new Vector3(player.transform.position.x, player.transform.position.y, -10) + offset;

        transform.position = moved;

        /**if ((moved.x - cam.orthographicSize * cam.aspect) >=-1.75f&& (moved.y - cam.orthographicSize) >= -6.3f)
        {

            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10) + offset;

        }
        else
        {
            if ((moved.y - cam.orthographicSize) >=-6.3f)
            {
                transform.position = new Vector3(transform.position.x, player.transform.position.y, -10) + offset;
            }
            else {


                if((moved.x - cam.orthographicSize * cam.aspect)>=1.75f)
                {
                    transform.position = new Vector3(player.transform.position.x,transform.position.y, -10) + offset;
                }
                else {
                    transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                }
            }

            
        }**/

    }
}