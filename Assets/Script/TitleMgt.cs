using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMgt : MonoBehaviour
{
    bool canMove;
    public Image icon;
    public Image icon2;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        icon2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
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
                FadeManager.Instance.LoadScene("Main", 1.0f);
            }
            else
            {
                GameEnd();
            }
        }
    }

    public void GameEnd()
    {
		Application.Quit();
    }

}
