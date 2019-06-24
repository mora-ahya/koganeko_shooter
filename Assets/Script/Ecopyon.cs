using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ecopyon : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 velocity = new Vector3(0, 0, 0);
    static float d = 1.0f;
    public GameObject prefabStar;
    public Text prefabText;
    Text pointText;
    GameObject[] stars;
    GameObject tmp;
    int point = 1;
    //public static int Count = 0;
    Vector3 p;
    public bool removeFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(offset);
        pointText = Instantiate(prefabText);
        stars = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            stars[i] = Instantiate(prefabStar);
        }
        transform.Translate(50, 50, 50);
        removeFlag = true;
        GetComponent<SpriteRenderer>().enabled = false;
        //GetComponent<BoxCollider2D>().enabled = false;
        //Count++;
    }

    public void Set(Vector3 p)
    {
        velocity.Set(0.05f * d, 0, 0);
        d = -d;
        this.p = p;
        transform.position = p;
        removeFlag = false;
        GetComponent<SpriteRenderer>().enabled = true;
        //GetComponent<BoxCollider2D>().enabled = true;
        point = 1;
        //pointText
    }

    public void Reset()
    {
        removeFlag = true;
        GetComponent<SpriteRenderer>().enabled = false;
        transform.Translate(50, 50, 50);
        //GetComponent<BoxCollider2D>().enabled = false;
        point = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!removeFlag)
        {
            p += velocity;
            if (p.x < -2.7f || p.x > 2.7f)//画面端跳ね返り
            {
                velocity.x = -velocity.x;
            }

            if (p.y < -2.2f) //画面下跳ね返り
            {
                p.y = -2.2f;
                velocity.y = -velocity.y * 0.9f;
            }
            else
            {
                velocity.y -= 0.003f;
            }
            transform.position = p;
            //Debug.Log(true);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!removeFlag)
        {
            if (other.gameObject.tag == "shot" && !other.gameObject.GetComponent<Shot>().removeFlag) //自機弾に触れたとき
            {
                GameManagement.Score += 1;
                //Count--;
                velocity.Set(p.x / 3.0f * 400, p.y / 2.4f * 320, 0);
                pointText.GetComponent<Point>().Set(1, velocity);
                //tmpT.rectTransform.localPosition -= offset;
                MakeStar();
                removeFlag = true;
                GetComponent<SpriteRenderer>().enabled = false;
                //GetComponent<BoxCollider2D>().enabled = false;
                other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                other.gameObject.GetComponent<Shot>().removeFlag = true;
                other.gameObject.GetComponent<Shot>().transform.Translate(75, 75, 75);
                transform.Translate(50, 50, 50);
            }
            else if (other.gameObject.tag == "star" && !other.gameObject.GetComponent<Star>().removeFlag) //自機弾に触れたとき
            {
                point = other.gameObject.GetComponent<Star>().point;
                GameManagement.Score += point;
                velocity.Set(p.x / 3.0f * 400, p.y / 2.4f * 320, 0);
                pointText.GetComponent<Point>().Set(point, velocity);
                //tmpT.rectTransform.localPosition -= offset;
                MakeStar();
                removeFlag = true;
                GetComponent<SpriteRenderer>().enabled = false;
                other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                other.gameObject.GetComponent<Star>().removeFlag = true;
                other.gameObject.GetComponent<Star>().transform.Translate(100f, 100f, 100f);
                transform.Translate(50, 50, 50);
                //GetComponent<BoxCollider2D>().enabled = false;
                //Debug.Log(1);
                //Count--;
            }
            if (GameManagement.Score > 99999999)
            {
                GameManagement.Score = 99999999;
            }
        }
    }

    void MakeStar()
    {
        point *= 2;
        if (point > 9999)
        {
            point = 9999;
            //Debug.Log(true);
        }
        for (int i = 0; i < 4; i++)
        {
            stars[i].GetComponent<Star>().Set(p, point);
        }

    }

    public bool CheckeStar()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!stars[i].GetComponent<Star>().removeFlag)
            {
                return false;
            }
        }
        return true;
    }
}
