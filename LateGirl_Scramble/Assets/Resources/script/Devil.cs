using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour {

    private float count = 0;
    private float milite1 = 0.1f;
    private bool isAnime = false;
    bool countMode=false;
    [SerializeField]
    Timer time;
	void Start () {
        countMode = false;
        isAnime = false;
        time.GetComponent<Timer>();
	}
	
	void Update () {
        if (!isAnime&&!countMode)
        {
            Debug.Log("あいや");
            ReSetAnime("Idle");
            SetAnime("Attack");
            count = 0;
            countMode = true;
        }
        if (isAnime&&!countMode)
        {
            ReSetAnime("Attack");
            SetAnime("Idle");
            count = 0;

            countMode = true;
        }
        count += Time.deltaTime;
        if (countMode)
        {
            if (isAnime && count >= 1f) isAnime = false;
            else if(count >= 0.5f) isAnime = true;
            countMode = false;

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
