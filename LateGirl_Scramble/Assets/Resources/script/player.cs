using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    [SerializeField]
    private float speed = 0f; //プレイヤーの速さ
    [SerializeField]
    float jumpH = 7f;//ジャンプの高さ


    [SerializeField]
    private AudioClip seJump;//ジャンプのSE
    [SerializeField]
    private AudioClip seSliding;//スライディングのSE
    [SerializeField]
    private AudioClip seAccid;//ぶつかった時のSE
    [SerializeField]
    private AudioClip seLand;//着地のSE
    [SerializeField]
    private AudioClip seDoubleJump;//二段ジャンプのSE
    [SerializeField]
    private AudioClip seChaim;//チャイムのSE
    [SerializeField]
    private AudioClip sePan;//パンのSE
    [SerializeField]
    private AudioClip seSukebo;//スケボーのSE
    private AudioSource audio;//これにPlay--で音を流せる

    GameObject itemObject;
    private string animeState = "";
    Item item;//Itemスクリプトから何に当たったかを確認するため
    string name;// なににあたったかをいれる箱
    private const int maxjump = 2;//二回ジャンプ
    private int jumpcount=0;//ジャンプのカウント
    private int panCount = 0;//panをとった数をカウント
    float count;//時間のカウント
    float sukeboCount;//スケボーの時間をカウント
    float sukeboTime=5f;//スケボーの時間制限
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
    private ScoreManager Score;
    [SerializeField]
    private Timer time;//timerスクリプトから時間を持ってくるように作成
    private BackSpeed backSpeed;
    
    // Use this for initialization
    void Start()
    {
        backSpeed = GameObject.Find("BackGround").gameObject.GetComponent<BackSpeed>();
        audio = GetComponent<AudioSource>();
        Xposition = transform.position.x;
        Score.panScore(0);
        panCount = 0;
        time.gorlGone(false);
        animeState = "ground";
        //デバッグ
        Debug.Log(transform.position.x);
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
            time.gorlGone(isGorl);
        }
        
        count += Time.deltaTime;
        //障害物に当たったら時間でアニメーションを変える
        if (isStop==true)
        {
            //miniteで何秒後に点滅解除
            if (count >= minite)
            {
                animeState = "ground";
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

            animeState = "ground";
            ReSetAnime("sukebo");
            ReSetAnime("sukeboJump");
            ReSetAnime("sukeboDoubleJump");
            SetAnime("groundtorriger");
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

                animeState = "sukeboJump";
                audio.PlayOneShot(seJump);
                SetAnime("sukeboJump");
                ReSetAnime("sukebo");
                isjump = true;
                GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, jumpH, 0);
            }
            else
            {
                animeState = "sukeboDoubleJump";
                audio.PlayOneShot(seDoubleJump);
                isdoublejump = true;
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
            isSriding = false;
            ReSetAnime("groundtorriger");
            ReSetAnime("sridingtorriger");
            jumpcount++;
            if (jumpcount == 1)
            {
                animeState = "jump";
                isjump = true;
                audio.PlayOneShot(seJump);
                SetAnime("jumptorriger");
                GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, jumpH, 0);
            }
            else
            {

                animeState = "DoubleJump";
                audio.PlayOneShot(seDoubleJump);
                Debug.Log(jumpcount);
                ReSetAnime("jumptorriger");
                SetAnime("doublejumptorriger");
                GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, jumpH, 0);
                isdoublejump = true;
            }
        }
    }

    public void Sriding()
    {
        //ここに矢印上 if (!Input.GetKeyDown("UpArrow")) return;
        if (isjump|| isStop||isdoublejump|| jumpcount != 0) { return; }
        if (Input.GetKey("a")&&animeState!="sriding")
        {
            animeState = "sriding";
            audio.PlayOneShot(seSliding);
            SetAnime("sridingtorriger");
            ReSetAnime("groundtorriger");
            isSriding = true;
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
            {
                ReSetAnime("sridingtorriger");
                isSriding = false;
                return;
            }
        }
        if (Input.GetKeyUp("a"))
        {
            animeState = "ground";
            SetAnime("groundtorriger");
            ReSetAnime("sridingtorriger");
            isSriding = false;
        }

    }
    //接触したらジャンプができる。後々グラウンドタグをつけていきたい(つけた)
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Ground"|| isStop||isSriding) return;
        if (!isSriding)
        {
            audio.PlayOneShot(seLand);
        }
        if (isSukebo)
        {
            SetAnime("sukebo");
            jumpcount = 0;
            SetAnime("groundtorriger");
            return;
        }
        if(jumpcount != 0&&Input.GetKey("a"))
        {
            animeState = "sriding";
            SetAnime("sridingtorriger");
            ReSetAnime("jumptorriger");
            ReSetAnime("doublejumptorriger");
            isjump = false;
            isdoublejump = false;
            jumpcount = 0;
            return;
        }
        animeState = "ground";
        SetAnime("groundtorriger");
        ReSetAnime("sridingtorriger");
        ReSetAnime("jumptorriger");
        ReSetAnime("doublejumptorriger");
        isjump = false;
        isdoublejump = false;
        jumpcount = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 

        if (collision.gameObject.tag == "Item") { 
            item = collision.gameObject.GetComponent<Item>();
            ItemGet();
        }
        if (speed!=0.0f) return;
        if (isSukebo) return;
        if (collision.gameObject.tag != "Block"&collision.gameObject.tag != "car") return;
        audio.PlayOneShot(seAccid);
        isStop = true;
        isjump = false;
        isSriding = false;
        Debug.Log("障害物に当たり申した");
        count = 0;
        speed = -0.1f;
        jumpcount = 0;
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
        audio.PlayOneShot(seSukebo);
        isSukebo = true;
        GetComponent<Animator>().SetBool("sukeboBool", isSukebo);
        SetAnime("sukebo");
        ReSetAnime("groundtorriger");
        ReSetAnime("sridingtorriger");
        ReSetAnime("jumptorriger");
        sukeboCount = 0;
        backSpeed.SpeedChange(0.1f);
    }
    private void pan()
    {

        audio.PlayOneShot(sePan);
        panCount++;
        backSpeed.PanSpeedUp();
        Score.panScore(panCount);
    }
    public int panReturn()
    {
        return panCount;
    }
    
    public void InGorl()
    {
        isGorl = true;
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
