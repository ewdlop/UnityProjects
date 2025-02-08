using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject player;

    public Camera cam;

    private Vector3 offset;



    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {

        transform.position = new Vector3(player.transform.position.x, 3.8f + 2.6f * 4 - 1.3f, -10);
        offset = transform.position - player.transform.position;
    }


    void LateUpdate()
    {

        Vector3 moved = new Vector3(player.transform.position.x, player.transform.position.y, -10) + offset;

        if ((moved.y + cam.orthographicSize) <= 25.9f&&(moved.y - cam.orthographicSize) >= 0)
        {

            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10) + offset;
            //transform.position = new Vector3(player.transform.position.x,player.transform.position.y-2.6f*12,-10)+offset;


        }
        else
        {

            transform.position = new Vector3(player.transform.position.x, transform.position.y, -10);
        }
    }
}