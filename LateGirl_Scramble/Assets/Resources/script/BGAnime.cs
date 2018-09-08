using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//　ゲームオブジェクトの種類。ここに置くことでprefabでも操作ができる
public enum Animation
{
    Bird,// 鳥のオブジェクト
    BirdSmall,// 小さい鳥のオブジェクト
    Cat, // 猫のオブジェクト
    jiji,// おじいちゃん
}

public class BGAnime : MonoBehaviour {
    // 
    public Animation kind;
    private string birdState;
    private bool isBird = false;

	// Use this for initialization
	void Start () {
        birdState = "Idle";
	}
	
	// Update is called once per frame
	void Update () {

        if (birdState == "Fly")
        {
            transform.Translate(-0.01f, 0.01f, 0);
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (kind == Animation.Bird)
        {
            if (collision.gameObject.tag == "Anime")
            {
             
                if ( Input.GetKeyDown(KeyCode.UpArrow) && !isBird)
                {
                    isBird = true;

                }
                if (isBird&& birdState == "Idle")
                {
                    SetAnime("Bird");
                    birdState = "Turn";
                    isBird = false;
                } else if (isBird && birdState == "Turn")
                {
                    birdState = "Fly";
                    SetAnime("Fly");
                    isBird = false;
                }

            }
        }else if (kind == Animation.BirdSmall)
        {
            if (collision.gameObject.tag == "Anime")
            {

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {

                    SetAnime("Bird");
                    birdState = "Fly";
                    SetAnime("Fly");
                }

            }
        }else if(kind == Animation.Cat)
        {
            if ( Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("down"))
            {
                SetAnime("Cat");
            }
            else
            {
                ReSetAnime("Cat");
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
