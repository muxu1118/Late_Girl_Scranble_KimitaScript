using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    [SerializeField]
    private float speed = 0.2f; //プレイヤーの速さ
    [SerializeField]
    float jumpH = 7f;//ジャンプの高さ
    public Sprite[] walk; //プレイヤーの歩くスプライト配列
    int animIndex; //歩くアニメーションのインデックス
    bool walkCheck; //歩いているかのチェック
    public float flame = 0f;//フレームチェック

    private const int maxjump = 2;//二回ジャンプ
    private int jumpcount=0;//ジャンプのカウント
    float count;//時間のカウント
    [SerializeField]
    float minite=3;//障害物にあたって止まる時間

    bool isjump = false;//ジャンプのbool
    bool isdoublejump = false;//ダブルジャンプ
    [SerializeField]
    bool isSriding = false;

    bool isStop = false;//障害物あたったかどうか

    [SerializeField]
    private Timer time;//timerスクリプトから時間を持ってくるように作成
    private BackGroundMove backSpeed;

    // Use this for initialization
    void Start()
    {
        //デバッグ
        Debug.Log(time.Count);
    }

    // Update is called once per frame
    void Update()
    {

        
        //ゲーム中だけ動かせるよう
        if (time.Count > time.CountLimit)
        {
            Jump();
            Sriding();
        }
        //ゲーム終了時にどっか行くように
        if (time.Count <= time.CountLimit)
        {
            transform.Translate(0.2f, 0, 0);
        }
        count += Time.deltaTime;
        //障害物に当たったら時間でアニメーションを変える
        if (isStop==true)
        {
            //miniteで何秒後に点滅解除
            if (count >= minite)
            {
                GetComponent<Animator>().SetTrigger("groundtorriger");
                GetComponent<Animator>().ResetTrigger("stop");
                Debug.Log(count);
                isStop = false;
            }
        }
    }

    //ジャンプをするよ
    public void Jump()
    {

        if (isStop || jumpcount >= maxjump) { return; }
        //クリック、スペースキーを押したとき
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {
            isjump = true;
            GetComponent<Animator>().ResetTrigger("groundtorriger");
            jumpcount++;
            if (jumpcount == 1)
            {
                GetComponent<Animator>().SetTrigger("jumptorriger");
                GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, jumpH, 0);
            }
            else
            {
                Debug.Log(jumpcount);
                GetComponent<Animator>().SetTrigger("doublejumptorriger");
                GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, jumpH, 0);

            }
        }
    }

 public void Sriding()
    {
        //ここに矢印上 if (!Input.GetKeyDown("UpArrow")) return;
        if (isjump|| isStop) { return; }
        if (Input.GetKey("a"))
        {
            GetComponent<Animator>().SetTrigger("sridingtorriger");
            GetComponent<Animator>().ResetTrigger("groundtorriger");
            isSriding = true;
            
        }
        else {
            GetComponent<Animator>().SetTrigger("groundtorriger");
            isSriding = false;
        }
        if (jumpcount != 0) GetComponent<Animator>().SetTrigger("groundtorriger");

    }
    //接触したらジャンプができる。後々グラウンドタグをつけていきたい(つけた)
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Ground"|| isStop) return;
        Debug.Log("ground");
        GetComponent<Animator>().SetTrigger("groundtorriger");
        GetComponent<Animator>().ResetTrigger("jumptorriger");
        GetComponent<Animator>().ResetTrigger("doublejumptorriger");
        isjump = false;
        jumpcount = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag != "Block"&collision.gameObject.tag != "car") return;
        isStop = true;
        isjump = false;
        isSriding = false;
        Debug.Log("障害物に当たり申した");
        count = 0;
        GetComponent<Animator>().SetTrigger("stop");
        GetComponent<Animator>().ResetTrigger("groundtorriger");
        GetComponent<Animator>().ResetTrigger("sridingtorriger");
        GetComponent<Animator>().ResetTrigger("jumptorriger");
    }
    ///前に作ったコマ割りでアニメーション動かすプログラム(使いません)
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
