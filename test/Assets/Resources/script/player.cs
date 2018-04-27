using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour 
    {
    [SerializeField]
    float speed = 0f; //プレイヤーの速さ
    public Sprite[] walk; //プレイヤーの歩くスプライト配列
    int animIndex; //歩くアニメーションのインデックス
    bool walkCheck; //歩いているかのチェック
    public float flame = 0f;

    // Use this for initialization
    void Start()
    {
        animIndex = 0;
        walkCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        flame += 0.1f;
        if (flame <= 20)
        {
            GetComponent<SpriteRenderer>().sprite = walk[0];
        }else if (flame >= 20||flame <= 40)
        {
            GetComponent<SpriteRenderer>().sprite = walk[1];
        }else if (flame <= 60|| flame >= 40)
        {
            GetComponent<SpriteRenderer>().sprite = walk[2];
        }
        else
        {
            flame = 0;
        }
    }
    void Walk()
    {
        
        //歩くアニメーションの再生
        animIndex++;
        if (animIndex >= walk.Length)
        {
            animIndex = 0;
        }
        GetComponent<SpriteRenderer>().sprite = walk[animIndex];

    }
    //「コルーチン」で呼び出すメソッド
    IEnumerator sleep()
    {
        yield return new WaitForSeconds(5);  //1秒待つ
    }
}
