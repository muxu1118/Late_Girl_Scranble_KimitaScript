using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour {

    private float count = 0;
    private float milite1 = 0.1f;
    private bool isAnime = false;
    bool isfly=false;
    bool countMode=false;
    bool isUp=false;//falseはdown　trueはup
    bool isSpwan=false;
    [SerializeField]
    Timer time;
    [SerializeField]
    GameObject smoke;
    [SerializeField]
    GameObject car;
    [SerializeField]
    GameObject objectParent;
    void Start () {
        countMode = true;
        count = 0;
        isAnime = false;
        time.GetComponent<Timer>();
	}

    void Update()
    {
        if(time.Count <= 120)
        {
            if (transform.position.x >= 7.38f && time.Count >= 115)
            {
                transform.Translate(-0.01f, 0, 0);
            }
            else isfly = true;
            if (transform.position.x <= 10.38f && time.Count <= 115)
            {
                transform.Translate(0.02f, 0, 0);
            }
        }
        
        if (isfly)
        {
            if (transform.position.y <= 2.4f)
            {
                isUp = true;
            }
            if (transform.position.y >= 2.79f)
            {
                isUp = false;
            }
            if (isUp)
            {
                transform.Translate(0, 0.03f, 0);
            }
            else
            {
                transform.Translate(0, -0.03f, 0);
            }
        }
        if (time.Count > 119f||time.Count<115) return;
        if (!isAnime && !countMode)
        {
            ReSetAnime("Idle");
            SetAnime("Attack");
            count = 0;
            countMode = true;
            if (!isSpwan)
            {
                Instantiate(smoke, new Vector3(7.38f, -4.0f, 0f), Quaternion.identity);
                GameObject obj = Instantiate(car, new Vector3(7.38f, -3.2f, 0f), Quaternion.identity);
                obj.transform.parent = objectParent.transform;
                isSpwan = true;
            }
        }
        if (isAnime && !countMode)
        {
            ReSetAnime("Attack");
            SetAnime("Idle");
            count = 0;
            countMode = true;
        }
        count += Time.deltaTime;
        if (countMode)
        {
            if (isAnime && count >= 1f) {
                Debug.Log("false");
                isAnime = false;
                count = 0;
                countMode = false;
            }
            if (!isAnime && count >= 0.5f) {
                Debug.Log("true");
                isAnime = true;
                count = 0;
                countMode = false;
            }
        }
        /*if (Input.GetKeyDown("x"))
        {
            ReSetAnime("Attack");
            SetAnime("Idle");
            

        }*/
    }

    private void SetAnime(string torriger)
    {
        GetComponent<Animator>().SetTrigger(torriger);
    }
    private void ReSetAnime(string torriger)
    {
        GetComponent<Animator>().ResetTrigger(torriger);
    }
}
