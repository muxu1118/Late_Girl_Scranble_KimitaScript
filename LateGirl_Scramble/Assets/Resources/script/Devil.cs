using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour {

    private float count = 0;
    private float milite1 = 0.1f;
    private bool isAnime = false;

	void Start () {
	}
	
	void Update () {
        if (Input.GetKeyDown("z"))
        {

            ReSetAnime("Idle");
            SetAnime("Attack");
            count = 0;
            isAnime = true;

        }
        count += Time.deltaTime;
        if (count>=milite1&& isAnime)
        {
            ReSetAnime("Attack");
            SetAnime("Idle");
            isAnime = false;
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
