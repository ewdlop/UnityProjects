using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    Animator anim;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        bool isWalking = (Mathf.Abs(inputX) + Mathf.Abs(inputY)) > 0;
        bool isFlying = false;


        anim.SetBool("isWalking", isWalking);
        if (isWalking)
        {
            if (inputX > 0)
            {

                transform.localScale = new Vector3(16, 16, 16);
                transform.position += new Vector3(1, 0, 0) * 16 * Time.deltaTime;
            }
            if (inputX < 0)
            {

                transform.localScale = new Vector3(-16, 16, 16);
                transform.position += new Vector3(-1, 0, 0) * 16 * Time.deltaTime;
            }

            if (inputY > 0 || isFlying==false)
            {
                isFlying = true;
                transform.position += new Vector3(0, 1, 0) *16*Time.deltaTime;
            }


            anim.SetFloat("x", inputX);
            anim.SetFloat("y", inputY);



            
        }
    }
}
