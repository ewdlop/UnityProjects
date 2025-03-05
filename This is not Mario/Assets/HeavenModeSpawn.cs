using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavenModeSpawn : MonoBehaviour {


    public GameObject missile;
    public GameObject smallmissile;
    public GameObject clouds;
    int[] random = new int[] {-1,1};
    GameObject clone;

	void Start () {
        InvokeRepeating("SpawnMissile", 5f, 0.25f);
        InvokeRepeating("SpawnsmallMissile", 5f, 0.05f);
        InvokeRepeating("SpawnsmallClouds", 5f, 0.25f);

    }
	
    void SpawnMissile()
    {
        int direction = random[Random.Range(0, random.Length)];
        clone = Instantiate(missile, new Vector3(230f+300f*direction, Random.Range(0,250f), -2f), Quaternion.identity);
        Rigidbody2D rigid=clone.GetComponent<Rigidbody2D>();
        rigid.linearVelocity = new Vector3(-1*direction*140f, 0, 0);
        clone.transform.localScale = new Vector3(50*direction, 50, 1);
    }
    void SpawnsmallMissile()
    {
        int direction = random[Random.Range(0, random.Length)];
        clone = Instantiate(smallmissile, new Vector3(230f + 300f * direction, Random.Range(0, 250f), -2f), Quaternion.identity);
        Rigidbody2D rigid = clone.GetComponent<Rigidbody2D>();
        rigid.linearVelocity = new Vector3(-1 * direction * 140f, 0, 0);
        clone.transform.localScale = new Vector3(10*direction, 10, 1);
    }

    void SpawnsmallClouds()
    {
        int direction = random[Random.Range(0, random.Length)];
        clone = Instantiate(clouds, new Vector3(230f + 300f * direction, Random.Range(0, 250f), -2f), Quaternion.identity);
        Rigidbody2D rigid = clone.GetComponent<Rigidbody2D>();
        rigid.linearVelocity = new Vector3(-1 * direction * 140f, 0, 0);
        clone.transform.localScale = new Vector3(20 * direction, 16, 1);
    }

}
