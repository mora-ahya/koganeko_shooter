using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Continue : MonoBehaviour
{
    public Text retry;
    public Text _return;
    public Image icon;
    public Image icon2;
    public GameObject mgt;
    public GameObject koganeko;
    bool canMove;
    bool draw;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        draw = false;
        retry.enabled = false;
        _return.enabled = false;
        icon.enabled = false;
        icon2.enabled = false;
        GetComponent<Text>().enabled = false;
    }

    void Reset()
    {
        canMove = true;
        draw = false;
        retry.enabled = false;
        _return.enabled = false;
        icon.enabled = false;
        icon2.enabled = false;
        GetComponent<Text>().enabled = false;
        mgt.GetComponent<GameManagement>().Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (koganeko.GetComponent<Koganeko>().endFlag && !draw)
        {
            StartCoroutine("Draw");
        }

        if (GetComponent<Text>().enabled) { 
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)) && canMove)
            {
                icon.enabled = !icon.enabled;
                icon2.enabled = !icon2.enabled;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                canMove = false;
                if (icon.enabled)
                {
                    Reset();
                }
                else
                {
                    FadeManager.Instance.LoadScene("TitleAnime", 1.0f);
                }
            }
        }
    }

    IEnumerator Draw()
    {
        draw = true;
        yield return new WaitForSeconds(3.0f);
        retry.enabled = true;
        _return.enabled = true;
        icon.enabled = true;
        GetComponent<Text>().enabled = true;
    }
}
