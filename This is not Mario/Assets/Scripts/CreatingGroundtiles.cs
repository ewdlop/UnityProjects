using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatingGroundtiles : MonoBehaviour {

    public GameObject Ground;
    

    public GameObject Fakeground;
    

    public GameObject Floor;
    

    public GameObject Questionblock;

    public GameObject FakeQuestionblock;


    public GameObject Fallingground;

    public GameObject Invisibleground;

    public GameObject gameControl;

    public Material mat;

    GameObject clone;

    // Use this for initialization
    void Start()
    {
        GameObject control = GameObject.Find("Control");

        //for (int i = 1; i < 30; i++)
        //{
        //    if (i == 17 || i == 18 || i == 19)
        //    {
        //        clone=Instantiate(Floor, new Vector3(1.3f + 2.55f * i, 3.9f + 2.6f * 4, 0), Quaternion.identity);
        //        if(control.GetComponent<Control>().hellmode == true)
        //        clone.GetComponent<SpriteRenderer>().material = mat;
        //        if(gameControl.GetComponent<ColorChange>().change)
        //        clone.GetComponent<SpriteRenderer>().color = Color.HSVToRGB((clone.transform.position.x + 98f) / (98f + 263f), 1f, 1f);
        //    }
        //    if (i == 22)
        //    {
        //        for (int k = 1; k < 9; k++)
        //        {

        //            if (k != 6)
        //            {

        //                clone = Instantiate(Floor, new Vector3(1.3f + 2.6f * i, 3.9f + 2.62f * k, 0), Quaternion.identity);
        //                if (control.GetComponent<Control>().hellmode == true)
        //                    clone.GetComponent<SpriteRenderer>().material = mat;
        //                if (gameControl.GetComponent<ColorChange>().change)
        //                    clone.GetComponent<SpriteRenderer>().color = Color.HSVToRGB((clone.transform.position.x + 98f) / (98f + 263f), 1f, 1f);
        //            }
        //        }
        //    }

        //    if (i == 2 || i == 3 || i == 4 || i == 5)
        //    {
        //        if (i != 2)
        //        {
        //            clone=Instantiate(Floor, new Vector3(1.3f + 7.8f + 2.55f * i, 2.6f + 5.2f, 0), Quaternion.identity);
        //            if (control.GetComponent<Control>().hellmode == true)
        //                clone.GetComponent<SpriteRenderer>().material = mat;
        //                if (gameControl.GetComponent<ColorChange>().change)
        //                clone.GetComponent<SpriteRenderer>().color = Color.HSVToRGB((clone.transform.position.x + 98f) / (98f + 263f), 1f, 1f);
        //        }
        //        else
        //        {
        //            clone = Instantiate(FakeQuestionblock, new Vector3(1.3f + 7.8f + 2.55f * i + 0.1f, 2.6f + 5.2f, 0), Quaternion.identity);
        //            if (control.GetComponent<Control>().hellmode == true)
        //                clone.GetComponent<SpriteRenderer>().material = mat;
        //            if (gameControl.GetComponent<ColorChange>().change)
        //                clone.GetComponent<SpriteRenderer>().color = Color.HSVToRGB((clone.transform.position.x + 98f) / (98f + 263f), 1f, 1f);
        //        }
        //    }

        //    if (i == 10 || i == 11 || i == 12)
        //    {
        //        clone = Instantiate(Floor, new Vector3(1.3f + 7.8f + 2.55f * i, 10.4f, 0), Quaternion.identity);
        //        if (control.GetComponent<Control>().hellmode == true)
        //            clone.GetComponent<SpriteRenderer>().material = mat;
        //        if (gameControl.GetComponent<ColorChange>().change)
        //            clone.GetComponent<SpriteRenderer>().color = Color.HSVToRGB((clone.transform.position.x + 98f) / (98f + 263f), 1f, 1f);
        //    }

        //}


        //for (int i = 35; i < 37; i++)
        //{
        //    clone = Instantiate(Invisibleground, new Vector3(1.3f + 2.62f * i, 1.3f, 0), Quaternion.identity);
        //    if (control.GetComponent<Control>().hellmode == true)
        //        clone.GetComponent<SpriteRenderer>().material = mat;
        //    //if (gameControl.GetComponent<ColorChange>().change)
        //    //    clone.GetComponent<SpriteRenderer>().color = Color.HSVToRGB((clone.transform.position.x + 98f) / (98f + 263f), 1f, 1f);
        //}

        
        

    }
}
