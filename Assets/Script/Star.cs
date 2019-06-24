using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 rotate = new Vector3(0, 0, 8);
    public int point = 2;
    int count;
    float r;
    public bool removeFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(100, 100, 100);
        GetComponent<SpriteRenderer>().enabled = false;
        removeFlag = true;
    }

    public void Set(Vector3 p, int point)
    {
        transform.position = p;
        this.point = point;
        r = Random.Range(0, 360);
        velocity.y = 0.1f * Mathf.Sin(r * Mathf.Deg2Rad);
        velocity.x = 0.1f * Mathf.Cos(r * Mathf.Deg2Rad);
        removeFlag = false;
        GetComponent<SpriteRenderer>().enabled = true;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!removeFlag)
        {
            transform.position += velocity;
            transform.Rotate(rotate);
            count++;
            if (count == 17)
            {
                removeFlag = true;
                GetComponent<SpriteRenderer>().enabled = false;
                transform.Translate(100f, 100f, 100f);
            }
            else if (transform.position.x < -2.7f || transform.position.x > 2.7f || transform.position.y < -2.2f || transform.position.y > 2.5f)
            {
                removeFlag = true;
                GetComponent<SpriteRenderer>().enabled = false;
                transform.Translate(100f, 100f, 100f);
            }
        }
    }
/*
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (enabled)
        {
            if (other.gameObject.tag == "ecopyon" || other.gameObject.tag == "koganeko") //敵or自機に触れたとき
            {
                enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
*/
}
