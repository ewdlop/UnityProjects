using UnityEngine;


public class SpawnBoo : MonoBehaviour
{
    public GameObject boo;
    bool spawn;

    void OnTriggerStay2D(Collider2D obstacle)
    {
        if (obstacle.gameObject.tag == "Player" && !spawn)
        {
            boo.SetActive(true);
            spawn = true;
            gameObject.SetActive(false);
        }

    }
}

