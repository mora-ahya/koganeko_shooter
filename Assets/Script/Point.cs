using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    Vector3 v = new Vector3(0, 0.1f, 0);
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
        GetComponent<Text>().enabled = false;
    }

    public void Set(int point, Vector3 p)
    {
        GetComponent<Text>().enabled = true;
        GetComponent<Text>().text = point.ToString();
        GetComponent<RectTransform>().localPosition = p;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Text>().enabled)
        {
            GetComponent<RectTransform>().localPosition += v;
            if (count == 100)
            {
                GetComponent<Text>().enabled = false;
            }
            count++;
        }
    }
}
