using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapShader : MonoBehaviour {

    public float radius;



    private void Update()
    {
        Material material = gameObject.GetComponent<SpriteRenderer>().material;
        material.SetFloat("_a", radius);
        material.SetFloat("_shipX", gameObject.transform.position.x);
        material.SetFloat("_shipY", gameObject.transform.position.y);
    }
}
