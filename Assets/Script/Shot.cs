using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 velocity = new Vector3(0, 0.1f, 0);
    Vector3 rotate = new Vector3(0, 0, 8);
    public bool removeFlag = true;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        removeFlag = true;
        transform.Translate(75, 75, 75);
    }

    public void Set(Vector3 p)
    {
        transform.position = p;
        removeFlag = false;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!removeFlag)
        {
            transform.position += velocity;
            transform.Rotate(rotate);
            if (transform.position.y > 2.5)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                removeFlag = true;
                transform.Translate(75, 75, 75);
            }
        }
    }
    /*
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!removeFlag)
        {
            if (other.gameObject.tag == "ecopyon" && !other.gameObject.GetComponent<Ecopyon>().removeFlag) //敵に触れたとき
            {
                GetComponent<SpriteRenderer>().enabled = false;
                removeFlag = true;
            }
        }
    }
    */
}
