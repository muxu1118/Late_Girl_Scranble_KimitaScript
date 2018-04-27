using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour 
    {
    [SerializeField]
    float speed = 0f; //プレイヤーの速さ
    [SerializeField]
    float jumpH = 5f;//ジャンプの高さ
    public Sprite[] walk; //プレイヤーの歩くスプライト配列
    int animIndex; //歩くアニメーションのインデックス
    bool walkCheck; //歩いているかのチェック
    public float flame = 0f;//フレームチェック

    bool isjump = false;//ジャンプのbool

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }
    

    //ジャンプをするよ（一回）
    public void Jump()
    {
        if (isjump) { return; }
        //クリック、スペースキーを押したとき
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {
            GetComponent<Animator>().SetTrigger("jumptorriger");
            GetComponent<Animator>().ResetTrigger("groundtorriger");
            GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, jumpH, 0);
            isjump = true;
            //参考演算子（ifみたいな）レイヤーが16だったら8にする
            gameObject.layer = gameObject.layer == 16 ? 8 : 16;
        }
    }
    //接触したらジャンプができる。後々グラウンドタグをつけていきたい
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Ground") return;
        Debug.Log("ground");
        GetComponent<Animator>().SetTrigger("groundtorriger");
        isjump = false;
    }
    ///前に作ったコマ割りでアニメーション動かすプログラム
     /* void update()
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
    */
}
