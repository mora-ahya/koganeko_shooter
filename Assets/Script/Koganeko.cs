using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Koganeko : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shot;
    public GameObject star;
    public GameObject mgt;
    public Text countText;
    GameObject[] shots;
    GameObject[] stars;
    Animator _animator;
    const float V_X = 0.05f;
    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 p = new Vector3(0, 0, 0);
    Vector3 cTp = new Vector3();
    int revivalCount = 0;
    int keyCount = 0;
    int invincibleCount;
    bool dying = false;
    bool invincible = false;
    public bool endFlag = false;
    WaitForSeconds wait = new WaitForSeconds(1.0f);

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        shots = new GameObject[1];
        shots[0] = Instantiate(shot);
        stars = new GameObject[6];
        for (int i = 0; i < 6; i++)
        {
            stars[i] = Instantiate(star);
        }
        revivalCount = 0;
        keyCount = 0;
        invincibleCount = 0;
        dying = false;
        invincible = false;
        endFlag = false;
        p = transform.position;
    }

    public void Reset()
    {
        revivalCount = 0;
        keyCount = 0;
        invincibleCount = 0;
        dying = false;
        invincible = false;
        endFlag = false;
        p.Set(0, -2.2f, 0);
        transform.position = p;
        GetComponent<SpriteRenderer>().enabled = true;
        _animator.SetBool("isDamaged", false);
        transform.Rotate(0, 0, -90);
    }

    // Update is called once per frame
    void Update()
    {
        if (!endFlag)
        {
            Key();
            RevivalChance();
            p += velocity;
            if (p.x < -2.7f)
            {
                p.x = -2.7f;
            } 
            else if (p.x > 2.7f)
            {
                p.x = 2.7f;
            }
            transform.position = p;
        }
    }

    void Key()
    {
        if (!dying)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                velocity.x = V_X;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                velocity.x = -V_X;
            }

            if (Input.GetKeyUp(KeyCode.RightArrow) && velocity.x == V_X || Input.GetKeyUp(KeyCode.LeftArrow) && velocity.x == -V_X)
            {
                velocity.x = 0;
            }

            if (Input.GetKeyDown(KeyCode.Space))//弾の生成
            {
                keyCount = CheckShots();
                if (keyCount == -1)
                {
                    keyCount = shots.Length;
                    Array.Resize(ref shots, keyCount + 1);
                    shots[keyCount] = Instantiate(shot);
                }
                shots[keyCount].GetComponent<Shot>().Set(p);
                keyCount = 0;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                keyCount++;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!dying && !invincible)
        {
            if (other.gameObject.tag == "ecopyon" && !other.gameObject.GetComponent<Ecopyon>().removeFlag ||
                other.gameObject.tag == "star" && !other.gameObject.GetComponent<Star>().removeFlag) //敵or星に触れたとき
            {
                dying = true;
                velocity.x = 0;
                _animator.SetBool("isDamaged", true);
                transform.Rotate(0, 0, 90);
                StartCoroutine("DeathCount");
            }
        }
        //Debug.Log(true);
    }

    bool RevivalChance()//規定回数以上スペースキーを押すことができれば復帰
    {
            if (keyCount >= revivalCount * 2 + 5)
            {
                _animator.SetBool("isDamaged", false);
                transform.Rotate(0, 0, -90);
                dying = false;
                invincible = true;
                keyCount = 0;
                revivalCount++;
                StartCoroutine("Invincible");
                return true;
            }
        return false;
    }

    private IEnumerator DeathCount()
    {
        countText.enabled = true;
        countText.GetComponent<Text>().text = "0";
        cTp.Set(p.x / 3.0f * 400, countText.rectTransform.localPosition.y, 0);
        countText.rectTransform.localPosition = cTp;
        yield return wait;
        //Debug.Log("1");
        if (!dying)
        {
            countText.enabled = false;
            yield break;
        }
        countText.GetComponent<Text>().text = "1";
        yield return wait;
        //Debug.Log("2");
        if (!dying)
        {
            countText.enabled = false;
            yield break;
        }
        countText.GetComponent<Text>().text = "2";
        yield return wait;
        //Debug.Log("3");
        if (!dying)
        {
            countText.enabled = false;
            yield break;
        }
        countText.GetComponent<Text>().text = "3";
        MakeStar();
        GetComponent<SpriteRenderer>().enabled = false;
        endFlag = true;
        yield return wait;
        countText.enabled = false;
    }

    private IEnumerator Invincible()
    {
        while (invincibleCount <= 49)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            //Debug.Log(true);
            yield return new WaitForSeconds(0.08f);
            invincibleCount++;
        }
        invincible = false;
        invincibleCount = 0;
    }

    void MakeStar()
    {
        for (int i = 0; i < 6; i++)
        {
            stars[i].GetComponent<Star>().Set(p, 1);
        }
        
    }

    int CheckShots()
    {
        for (int i = 0; i < shots.Length; i++)
        {
            if (shots[i].GetComponent<Shot>().removeFlag)
            {
                return i;
            }
        }

        return -1;
    }
}
