using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimeMgt : MonoBehaviour
{
    public GameObject koganeko;
    public GameObject ecopyon;
    public Text text;
    int phase = 0;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        phase = 0;
        count = 0;
        Screen.SetResolution(800, 680, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            phase = 5;
        }
        switch (phase)
        {
            case 0:
                koganeko.transform.position += new Vector3(-0.2f, 0, 0);
                count++;
                if (count == 40)
                {
                    phase++;
                    count = 0;
                }
                break;
            case 1:
                ecopyon.transform.Translate(0.2f, 0, 0);
                count++;
                if (count == 40)
                {
                    phase++;
                    count = 0;
                }
                break;
            case 3:
                text.rectTransform.localScale = new Vector3(1, text.rectTransform.localScale.y + 0.05f, 0);
                count++;
                if (count == 20)
                {
                    phase++;
                    count = 0;
                }
                break;
            case 2:
            case 4:
                count++;
                if (count == 20)
                {
                    phase++;
                    count = 0;
                }
                break;
            case 5:
                FadeManager.Instance.LoadScene("Title", 0.3f);
                break;
        }
    }
}
