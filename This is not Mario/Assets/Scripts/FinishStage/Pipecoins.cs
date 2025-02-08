using UnityEngine;

public class Pipecoins : MonoBehaviour
{
    public GameObject coins;
    GameObject clone;
    Rigidbody2D rigid;

    void Awake()
    {
        InvokeRepeating(nameof(LaunchWalls), 1f, 0.025f);
    }

    void LaunchWalls()
    {
        clone = Instantiate(coins, new Vector3(transform.position.x, transform.position.y, -0.1f), Quaternion.identity);

        if (clone.GetComponent<Rigidbody2D>() == null)
        {
            rigid = clone.AddComponent<Rigidbody2D>();
            if (gameObject.name == "VerticalPipes")
            {
                rigid.AddForce(new Vector3(Random.Range(-1f, 1f) * (15f + Random.value * 2f) / 10, 20 / 10f, 0), ForceMode2D.Impulse);
            }
            else
            {
                GameObject control = GameObject.Find("Control");
                if (control.GetComponent<Control>().heavenmode)
                {
                    rigid.AddForce(new Vector3((25f + Random.value * 2f) / 10, 7 / 10f, 0), ForceMode2D.Impulse);
                }
                else
                {
                    rigid.AddForce(new Vector3(25f + Random.value * 2f, 7f, 0), ForceMode2D.Impulse);
                }
            }
        }
        else
        {
            rigid = clone.GetComponent<Rigidbody2D>();
            if (gameObject.name == "VerticalPipes")
            {
                rigid.AddForce(new Vector3(Random.Range(-1f, 1f) * (15f + Random.value * 2f) / 10, 20 / 10f, 0), ForceMode2D.Impulse);
            }
            else
            {
                GameObject control = GameObject.Find("Control");
                if (control.GetComponent<Control>().heavenmode)
                {
                    rigid.AddForce(new Vector3((30f + Random.value * 2f) / 10, 7 / 10f, 0), ForceMode2D.Impulse);
                }
                else
                {
                    rigid.AddForce(new Vector3(15f + Random.value * 2f, 7f, 0), ForceMode2D.Impulse);
                }
            }
        }
    }
}
