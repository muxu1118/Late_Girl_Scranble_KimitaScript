using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class player : MonoBehaviour {
    [SerializeField]
    private float speed = 0f; //プレイヤーの速さ
    [SerializeField]
    float jumpH = 7f;//ジャンプの高さ


    [SerializeField]
    private AudioClip seJump;//ジャンプのSE
	[Range(0f,100f)][SerializeField]
	private float seJumpVol;
    [SerializeField]
    private AudioClip seSliding;//スライディングのSE
	[Range(0f, 100f)][SerializeField]
	private float seSlidingVol;
    [SerializeField]
	private AudioClip seAccid;//ぶつかった時のSE
	[Range(0f,100f)][SerializeField]
	private float seAccidVol;
    [SerializeField]
    private AudioClip seLand;//着地のSE
	[Range(0f, 100f)][SerializeField]
	private float seLandVol;
    [SerializeField]
    private AudioClip seDoubleJump;//二段ジャンプのSE
    [Range(0f,100f)][SerializeField]
	private float seDoubleJumpVol;
    [SerializeField]
    private AudioClip seChaim;//チャイムのSE
    [Range(0f,100f)][SerializeField]
	private float seChaimVol;
    [SerializeField]
    private AudioClip sePan;//パンのSE
    [Range(0f,100f)][SerializeField]
	private float sePanVol;
    [SerializeField]
    private AudioClip seSukebo;//スケボーのSE
    [Range(0f,100f)][SerializeField]
	private float seSukeboVol;
    private AudioSource audio;//これにPlay--で音を流せる

    GameObject itemObject;
    private string animeState = "";
    Item item;//Itemスクリプトから何に当たったかを確認するため
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
    public static bool isSukebo = false;
    bool isSukeboJump = false;
    bool muteki = false;
    float mutekiCount=0;
    float mutekiTime = 0;
    [SerializeField]
    private ScoreManager Score;
    [SerializeField]
    private Timer time;//timerスクリプトから時間を持ってくるように作成
    private BackSpeed backSpeed;
    [SerializeField]
    Sprite sp;
    [SerializeField]
    Sprite playerSp;
    SpriteRenderer sr;
    // Use this for initialization
    void Start()
    {
        isGorl = false;
        backSpeed = GameObject.Find("BackGround").gameObject.GetComponent<BackSpeed>();
        audio = GetComponent<AudioSource>();
        Xposition = transform.position.x;
        Score.panScore(0);
        panCount = 0;
        time.gorlGone(false);
        animeState = "ground";
        sr = gameObject.GetComponent<SpriteRenderer>();
        //デバッグ
        Debug.Log(transform.position.x);

    }
    // Update is called once per frame
    void Update()
    {
        if (!CountDown.isStart) return;
        transform.Translate(speed, 0, 0);
        //ゲーム中だけ動かせるよう
        if (!isGorl&&!isSukebo){
            Jump();
            Sriding();
        }
        else if(isSukebo)//スケボー時にできる
        {
            sukeboJump();
            sukeboCount += Time.deltaTime;
            if (sukeboCount >= sukeboTime)
            {
                SukeboRelease();
            }
        }

        //ゲーム終了時にどっか行くように
        if (isGorl)
        {
            SukeboRelease();
            time.gorlGone(isGorl);
        }
        //時間カウント系
        mutekiCount += Time.deltaTime;
        count += Time.deltaTime;
        //障害物に当たったら時間でアニメーションを変える
        if (isStop)
        {
            //miniteで何秒後に点滅解除
            if (count >= minite)
            {
                animeState = "ground";
                SetAnime("groundtorriger");
                ReSetAnime("stop");
                speed = 0.2f;
                mutekiCount = 0;
                Debug.Log(count);
                StartCoroutine(Tenmetu(minite, 0.1f));
                isStop = false;
                muteki = true;
            }
        }else
        {
            //いつもの位置についたら時にスピードを抑える
            if (transform.position.x >= Xposition)
            {
                speed = 0f;
            }
        }
        if (muteki)//miniteの時間の間無敵　※誤字は気にすんな！
        {
            if (mutekiCount >= minite+2f)
            {
                mutekiTime = 0;
                muteki = false;
            }
        }
    }
    //スケボー状態解除
    private void SukeboRelease()
    {
        backSpeed.SpeedChange(0f); 
        Debug.Log("スケボー解除");
        isSukebo = false;
        GetComponent<Animator>().SetBool("sukeboBool", isSukebo);

        animeState = "ground";
        ReSetAnime("sukebo");
        ReSetAnime("sukeboJump");
        SetAnime("groundtorriger");
        muteki = true;
        mutekiCount = 0;
        if (jumpcount != 0)
        {
            SetAnime("jumptorriger");
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
        if (speed != 0.0f) return;
        if (muteki) return;
        if (collision.gameObject.tag != "Block"&collision.gameObject.tag != "car") return;
        if (isSukebo) return;
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

    private IEnumerator Tenmetu(float time,float wait) {
        float nowTime = 0;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        while (time > nowTime) {

            nowTime += Time.deltaTime;
            yield return new WaitForSeconds(wait);
            Color color = sr.color;
            color.a = 0;
            sr.color = color;
            nowTime += Time.deltaTime;
            yield return new WaitForSeconds(wait);
            color.a = 1;
            sr.color = color;
            nowTime += Time.deltaTime;

            //yield return StartCoroutine(Waiting(1.0f));
        }
    }

    private IEnumerator Waiting(float value) {
        yield return new WaitForSeconds(value);
    }

}
