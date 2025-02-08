using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPMovement : MonoBehaviour
{
    public float speed;
    public float angularSpeed;
    public Camera topDown;
    public Camera firstPerson;
    public Vector3 topDownOffset;
    public Vector3 firstPersonOffset;

    void Update()
    {

        float speedZ = Input.GetAxis("Vertical") * speed;
        float angularSpeedY = Input.GetAxis("Horizontal") * angularSpeed;
        GetComponent<Rigidbody>().velocity = speedZ * transform.forward;
        GetComponent<Rigidbody>().angularVelocity = angularSpeedY * Vector3.up;
    }
}
