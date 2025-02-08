using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savepoint : MonoBehaviour {


    public int nthsavepoint;
    public int stage;

    private void Start()
    {
        if (GameControl.reachsavepoint[nthsavepoint, stage])
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&& !GameControl.reachsavepoint[nthsavepoint, stage]) {
            GameControl.stage = stage;
            GameControl.saveindex = nthsavepoint;
            GameControl.reachsavepoint[nthsavepoint,stage] =true;
            gameObject.SetActive(false);

        }
    }
}
