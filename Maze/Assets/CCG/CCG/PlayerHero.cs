using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHero : MonoBehaviour {

    void OnMouseOver()
    {
        GetComponent<MeshRenderer>().material.color = new Color(1f, 0f, 0f);
    }
}
