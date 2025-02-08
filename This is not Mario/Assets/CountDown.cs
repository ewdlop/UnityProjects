using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDown : MonoBehaviour {

    public float timeleft;
    public Text countdown;
    public GameObject player;
    public GameObject cannon;
    public Camera cam;
    

    Rigidbody2D rigid;
    public bool trigger=false;
    float distance = 0;

	
	void Start () {
        rigid = player.GetComponent<Rigidbody2D>();
        distance = 0;
        GameControl.distance = 0;
    }
	
	
	void Update () {

        timeleft -= Time.deltaTime;
        

        if (timeleft >= 0)
        {
            float timeleftint = Mathf.Floor(timeleft);
            countdown.text = timeleftint.ToString();
        }
        else
        {
            distance += Time.deltaTime;
            if (!trigger)
            {
                trigger = true;
                Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                float radius = Mathf.Sqrt(Mathf.Pow((worldPosition.y - cannon.transform.position.y), 2) + Mathf.Pow((worldPosition.x - cannon.transform.position.x), 2));
                rigid.AddForce(new Vector2( radius*2* Mathf.Cos(RotateCannon.angle*Mathf.PI/180), 2*radius* Mathf.Sin(RotateCannon.angle * Mathf.PI / 180)), ForceMode2D.Impulse);
                countdown.gameObject.SetActive(false);
            }
            if(!CharacterControllers.isDead)
                GameControl.distance = distance;
        }

    }
}
