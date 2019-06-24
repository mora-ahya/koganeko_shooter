using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//横3縦2.4
public class GameManagement : MonoBehaviour
{
    public static int Score = 0;
    public Text scoreText;
    public GameObject ecopyon;
    public GameObject koganeko;
    GameObject[] ecopyons;
    int count = 0;
    int interval;
    Vector3 v = new Vector3(0.2f, 0, 0);
    Vector3 p;
    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        scoreText.text = "Score: 00000000";
        interval = 30;
        ecopyons = new GameObject[1];
        ecopyons[0] = Instantiate(ecopyon);
    }

    public void Reset()
    {
        Score = 0;
        scoreText.text = "Score: 00000000";
        interval = 30;
        for (int i = 0; i < ecopyons.Length; i++)
        {
            ecopyons[i].GetComponent<Ecopyon>().Reset();
        }
        koganeko.GetComponent<Koganeko>().Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (!koganeko.GetComponent<Koganeko>().endFlag)
        {
            p = transform.position;

            if (count > interval)
            {
                count = CheckEcopyons();
                if (count == -1)
                {
                    count = ecopyons.Length;
                    Array.Resize(ref ecopyons, count + 1);
                    ecopyons[count] = Instantiate(ecopyon);
                }
                ecopyons[count].GetComponent<Ecopyon>().Set(p);
                count = 0;
            }

            if (p.x > 2.5f)
            {
                p.x = 2.5f;
                v.x = -v.x;
            }
            else if (p.x < -2.5f)
            {
                p.x = -2.5f;
                v.x = -v.x;
            }
            p += v;
            transform.position = p;
            count++;
            interval = (int)(15 * 10000d / (Score + 1));
            //Debug.Log(interval);
            if (interval > 30)
            {
                interval = 30;
            }
            else if (interval < 5)
            {
                interval = 5;
            }
        }
        scoreText.text = "Score: " + Score.ToString().PadLeft(8, '0');
    }

    int CheckEcopyons()
    {
        for (int i = 0; i < ecopyons.Length; i++)
        {
            if (ecopyons[i].GetComponent<Ecopyon>().removeFlag && ecopyons[i].GetComponent<Ecopyon>().CheckeStar())
            {
                return i;
            }
        }

        return -1;
    }
}
