using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Animation
{
    Bird,
    Cat,
    jiji,
}

public class BGAnime : MonoBehaviour {

    public Animation kind;
    private string birdState;
    private bool isBird = false;

	// Use this for initialization
	void Start () {
        birdState = "Idle";
	}
	
	// Update is called once per frame
	void Update () {

		
	}
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (kind == Animation.Bird)
        {
            if (collision.gameObject.tag == "Zone")
            {

                Debug.Log("2" + birdState);
                if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.UpArrow)) && !isBird)
                {
                    Debug.Log(birdState);
                    isBird = true;

                }
                if (isBird&& birdState == "Idle")
                {
                    Debug.Log(birdState);
                    SetAnime("Bird");
                    birdState = "Turn";
                    isBird = false;
                } else if (isBird && birdState == "Turn")
                {
                    Debug.Log(birdState);
                    birdState = "Fly";
                    SetAnime("Fly");
                    isBird = false;
                }
            }
        }
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
