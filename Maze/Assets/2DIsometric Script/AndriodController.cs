using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndriodController : MonoBehaviour {

	
	void Update () {
		if(Application.platform == RuntimePlatform.Android)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
	}
}
