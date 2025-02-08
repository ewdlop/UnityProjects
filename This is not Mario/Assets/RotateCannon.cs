using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateCannon : MonoBehaviour {

    public static float angle;
    public Camera cam;
    LineRenderer line;
    float radius;
    public Text angletext;

	void Update () {


        Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(worldPosition.y-transform.position.y, worldPosition.x - transform.position.x) *180/Mathf.PI;
        transform.eulerAngles=new Vector3(0, 0, angle-90);
        line = GetComponent<LineRenderer>();
        line.SetPosition(1, new Vector3(worldPosition.x, worldPosition.y,0));
        angletext.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        radius = Mathf.Sqrt(Mathf.Pow((worldPosition.y - transform.position.y),2)+ Mathf.Pow((worldPosition.x - transform.position.x), 2));
        angletext.text = "<"+(radius*2).ToString()+","+angle.ToString()+ "\u00B0"+">";
    }
}
