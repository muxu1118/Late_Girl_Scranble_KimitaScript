using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    [SerializeField]
    private float speed = 0f; //プレイヤーの速さ
    [SerializeField]
    float jumpH = 7f;//ジャンプの高さ
    

    GameObject itemObject;
    Item item;//Itemスクリプトから何に当たったかを確認するため
    string name;// なににあたったかをいれるはこ
    private const int maxjump = 2;//二回ジャンプ
    private int jumpcount=0;//ジャンプのカウント
    private int panCount = 0;//panをとった数をカウント
    float count;//時間のカウント
    float sukeboCount;//スケボーの時間をカウント
    float sukeboTime=10f;//スケボーの時間制限
    [SerializeField]
    float minite=3;//障害物にあたって止まる時間
    float Xposition;
    bool isjump = false;//ジャンプのbool
    bool isdoublejump = false;//ダブルジャンプ
    [SerializeField]
    bool isSriding = false;
    bool isGorl = false;//ゴールしてる状態かどうか
    bool isStop = false;//障害物あたったかどうか
    bool isItem = false;//アイテムをとった状態かどうか
    bool isSukebo = false;
    bool isSukeboJump = false;

    [SerializeField]
    private Timer time;//timerスクリプトから時間を持ってくるように作成
    private BackSpeed backSpeed;

    // Use this for initialization
    void Start()
    {
        backSpeed = GameObject.Find("BackGround").gameObject.GetComponent<BackSpeed>();
        Xposition = transform.position.x;
        //デバッグ
        Debug.Log(time.Count);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed, 0, 0);
        //ゲーム中だけ動かせるよう
        if (time.Count > time.CountLimit&&!isSukebo){
            Jump();
            Sriding();
        }
        else if(isSukebo)
        {
            sukeboJump();
            sukeboCount += Time.deltaTime;
            SukeboRelease();
        }
        else {
            isGorl = true;
        }
        //ゲーム終了時にどっか行くように
        if (isGorl)
        {
            speed = 0.2f;
        }
        
        count += Time.deltaTime;
        //障害物に当たったら時間でアニメーションを変える
        if (isStop==true)
        {
            //miniteで何秒後に点滅解除
            if (count >= minite)
            {
                SetAnime("groundtorriger");
                ReSetAnime("stop");
                speed = 0.2f;
                Debug.Log(count);
                isStop = false;
            }
        }else if(!isGorl)
        {
            if (transform.position.x >= Xposition)
            {
                speed = 0f;
            }
        }
    }
    //スケボー状態解除
    private void SukeboRelease()
    {
        if (sukeboCount >= sukeboTime)
        {
            backSpeed.SpeedChange(0f); 
            Debug.Log("スケボー解除");
            isSukebo = false;
            GetComponent<Animator>().SetBool("sukeboBool", isSukebo);
            ReSetAnime("sukebo");
            if (jumpcount != 0)
            {
                SetAnime("jumptorriger");
            }
        }
    }
    //ジャンプをするよ
    private void sukeboJump()
    {

        if (jumpcount >= maxjump) { return; }
        //クリック、スペースキーを押したとき
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {
            ReSetAnime("groundtorriger");
            jumpcount++;
            if (jumpcount == 1)
            {
                SetAnime("sukeboJump");
                ReSetAnime("sukebo");
                GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, jumpH, 0);
            }
            else
            {
                Debug.Log(jumpcount);
                SetAnime("sukeboDoubleJump");
                ReSetAnime("sukeboJump");
                GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, jumpH, 0);

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
            isSriding = false;
            ReSetAnime("groundtorriger");
            ReSetAnime("sridingtorriger");
            jumpcount++;
            if (jumpcount == 1)
            {
                SetAnime("jumptorriger");
                GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, jumpH, 0);
            }
            else
            {
                Debug.Log(jumpcount);
                SetAnime("doublejumptorriger");
                GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, jumpH, 0);

            }
        }
    }

    public void Sriding()
    {
        //ここに矢印上 if (!Input.GetKeyDown("UpArrow")) return;
        if (isjump|| isStop||isdoublejump) { return; }
        if (Input.GetKey("a"))
        {
            SetAnime("sridingtorriger");
            ReSetAnime("groundtorriger");
            ReSetAnime("jumptorriger");
            ReSetAnime("doublejumptorriger");
            isSriding = true;
            
        }
        else {
            SetAnime("groundtorriger");
            isSriding = false;
        }
        if (jumpcount != 0) SetAnime("groundtorriger");

    }
    //接触したらジャンプができる。後々グラウンドタグをつけていきたい(つけた)
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Ground"|| isStop) return;
        Debug.Log("ground");
        if (isSukebo)
        {
            SetAnime("sukebo");
            jumpcount = 0;
            SetAnime("groundtorriger");
            return;
        }
        SetAnime("groundtorriger");
        ReSetAnime("jumptorriger");
        ReSetAnime("doublejumptorriger");
        isjump = false;
        jumpcount = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item") { 
            item = collision.gameObject.GetComponent<Item>();
            ItemGet();
        }
        if (speed!=0.0f) return;
        if (collision.gameObject.tag != "Block"&collision.gameObject.tag != "car") return;
        isStop = true;
        isjump = false;
        isSriding = false;
        Debug.Log("障害物に当たり申した");
        count = 0;
        speed = -0.1f;
        SetAnime("stop");
        ReSetAnime("groundtorriger");
        ReSetAnime("sridingtorriger");
        ReSetAnime("jumptorriger");
        ReSetAnime("doublejumptorriger");
    }
    private void ItemGet()
    {
        switch (item.HeyItemName()){
            case 1:
                pan();
                break;
            case 2:
                sukebo();
                break;
            default:
                break;
        }

    }
    private void prasol()
    {
        Debug.Log("パラソル");
    }
    //スケボーに乗っている状態
    private void sukebo()
    {
        isSukebo = true;
        GetComponent<Animator>().SetBool("sukeboBool", isSukebo);
        SetAnime("sukebo");
        ReSetAnime("groundtorriger");
        ReSetAnime("sridingtorriger");
        ReSetAnime("jumptorriger");
        sukeboCount = 0;
        backSpeed.SpeedChange(0.2f);
    }
    private void pan()
    {
        panCount++;
        backSpeed.PanSpeedUp();
    }
    public int panReturn()
    {
        return panCount;
    }
    private void SetAnime(string torriger)
    {
        GetComponent<Animator>().SetTrigger(torriger);
    }
    private void ReSetAnime(string torriger)
    {
        GetComponent<Animator>().ResetTrigger(torriger);
    }
    ///前に作ったコマ割りでアニメーション動かすプログラム(使いません)
     /*
    public Sprite[] walk; //プレイヤーの歩くスプライト配列
    int animIndex; //歩くアニメーションのインデックス
    bool walkCheck; //歩いているかのチェック
    public float flame = 0f;//フレームチェック
    
    void update()
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
