using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Control : MonoBehaviour {


    public int stage;
    public bool hellmode;
    public bool heavenmode;
    public bool nightmaremode;

    private void Awake()
    {

        GameControl.stage = stage;
       
    }

}
