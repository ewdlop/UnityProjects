using UnityEngine;

public class SpawnGiantMissileBillsNightMareMode : MonoBehaviour {

    public GameObject GiantMissileBills;
    GameObject control;
    GameObject clone;
    GameObject clone2;
    Rigidbody2D rigid;
    bool spawn=false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        control = GameObject.Find("Control");
        if (spawn != true)
        {
            if (collision.gameObject.tag == "Player")
            {
                spawn = true;
                if (control.GetComponent<Control>().hellmode == true)
                {
                    clone = Instantiate(GiantMissileBills, new Vector3(transform.position.x - 20f, transform.position.y, -3), Quaternion.identity);
                    clone.transform.localScale = new Vector3(clone.transform.localScale.x * -1, clone.transform.localScale.y, clone.transform.localScale.z);
                    rigid = clone.GetComponent<Rigidbody2D>();
                    rigid.linearVelocity = new Vector2(100, 0);
                }
                    clone = Instantiate(GiantMissileBills, new Vector3(transform.position.x+20f,transform.position.y, -3), Quaternion.identity);
                    rigid = clone.GetComponent<Rigidbody2D>();
                    rigid.linearVelocity = new Vector2(-100,0);
                    
                    gameObject.SetActive(false);

           }
        }
    }
}
