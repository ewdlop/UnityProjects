using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setuplight : MonoBehaviour {

    public GameObject[] coins;
    public GameObject lightprefab;
    public GameObject[] lights;
    [Range(0.0f, 500.0f)]
    public float Range;
    [Range(0.0f, 200.0f)]
    public float Intensity;
    GameObject clone;
    // Use this for initialization
    void Start () {


        coins = GameObject.FindGameObjectsWithTag("Coin");

        foreach (GameObject coin in coins)
        {
            clone=Instantiate(lightprefab, new Vector3(coin.transform.position.x, coin.transform.position.y,-1f), coin.transform.rotation);
            clone.transform.parent = coin.transform;
        }

    }
	
	// Update is called once per frame
	void Update () {

        lights = GameObject.FindGameObjectsWithTag("Light");

        foreach (GameObject light in lights)
        {
            light.GetComponent<Light>().range = Range;
            light.GetComponent<Light>().intensity = Intensity;
        }


    }
}
