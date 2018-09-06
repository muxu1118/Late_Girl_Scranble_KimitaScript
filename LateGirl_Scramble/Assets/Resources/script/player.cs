using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;

public class player : MonoBehaviour {
    [SerializeField]
    private float speed = 0f; //プレイヤーの速さ
    [SerializeField]
    float jumpH = 14f;//ジャンプの高さ
    

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
    [Range(0f, 100f)][SerializeField]
    private float seSukeboVol;
    [SerializeField]
    private AudioClip seDash;//食パンダッシュのSE
    [Range(0f, 100f)][SerializeField]
    private float seDashvol;
    
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
    bool isStart = false;// 始まり
    public static bool isSukebo = false;
    bool isSukeboJump = false;
    bool muteki = false;
    bool isTime = false;
    bool isOkkake = false;
    float mutekiCount=0;
    float mutekiTime = 0;
    float sridingTime = 0;
    int boostState = 0;//食パンブーストの状態0:食0,1:食1,2:食2,3:食3
    float[] gageMator = new float[3];//食パンゲージのメーター最高1
    Rigidbody2D rb2d;
    [SerializeField]
    GameObject pauseWindow;// ポーズ画面かどうか
    private GameObject obj;

    [SerializeField]
    private Slider[] slider = new Slider[3];
    
    [SerializeField]
    private GameObject panDashEffect;
    [SerializeField]
    private GameObject sukeboEffect;
    [SerializeField]
    private ScoreManager Score;
    [SerializeField]
    private Timer time;//timerスクリプトから時間を持ってくるように作成
    [SerializeField]
    private CutIn Cut;
    private BackSpeed backSpeed;
    [SerializeField]
    Sprite sp;//スプライト
    [SerializeField]
    Sprite playerSp;//プレイヤーのスプライト
    SpriteRenderer sr;//スプライトレンダラー
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
        rb2d = GetComponent<Rigidbody2D>();

    }
    // Update is called once per frame
    void Update()
    {
        //カウントダウンとカットインの時は止まるように
        if (!CountDown.isStart || CutIn.isCutIn||isTime) return;
        if(time.Count <= 0.01f)
        {
            InTime();
        }
        if (!isStart)
        {
            isStart = true;
            SetAnime("Start");
        }
        if (pauseWindow.activeSelf) return;
        //これで下がったり下がらなかったり
        transform.Translate(speed, 0, 0);

        //ゲージ増やし
        GageManeger(true);
        GageManeger(false);

        //ゲーム中だけ動かせるよう
        if (!isGorl&&!isSukebo){
            PanDash();
            //ジャンプ
            Jump();
            //スライディング
            Sriding();
            sridingTime+=Time.deltaTime;
            SridingRelease(sridingTime,2.0f);
            if (isjump || isdoublejump) rb2d.gravityScale += 1f * Time.deltaTime;
        }
        else if(isSukebo)//スケボー時にできる
        {
            //スケボージャンプ
            sukeboJump();
            sukeboCount += Time.deltaTime;
            if (sukeboCount >= sukeboTime)
            {
                SukeboRelease();
            }
            if (isjump) rb2d.gravityScale += 1f * Time.deltaTime;
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
            if (count >= minite-2)
            {
                
                mutekiCount = 0;
                Debug.Log(count);
                StartCoroutine(Tenmetu(minite-0.7f, 0.15f));
                
            }
            if (count >= minite)
            {
                animeState = "ground";
                SetAnime("groundtorriger");
                ReSetAnime("stop");
                speed = 0.2f;
                isStop = false;
                muteki = true;
            }
        }
        else
        {
            //いつもの位置についたら時にスピードを抑える
            if (transform.position.x >= Xposition)
            {
                speed = 0f;
            }
        }
        if (muteki)//miniteの時間の間無敵　※誤字は気にすんな！
        {
            if (mutekiCount >= minite+0.8f)
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
        Destroy(obj);
        GetComponent<Animator>().SetBool("sukeboBool", isSukebo);
        if(!isGorl)
        StartCoroutine(Tenmetu(minite-0.5f, 0.1f));
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
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
                Debug.Log("1Jump");
                SetAnime("jumptorriger");
                GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, jumpH, 0);
            }
            else
            {
                rb2d.gravityScale = 1f;
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
        if ( Input.GetKeyDown(KeyCode.DownArrow)&& animeState!="sriding")
        {
            animeState = "sriding";
            audio.PlayOneShot(seSliding);
            SetAnime("sridingtorriger");
            ReSetAnime("groundtorriger");

            isSriding = true;
            sridingTime = 0;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                isSriding = false;
                return;
            }
        }

    }
    private void SridingRelease(float time,float limit)
    {
        if (time > limit&&isSriding)
        {
            if (Input.GetKey(KeyCode.DownArrow))//keycodeに変更
            {
                animeState = "sriding";
                SetAnime("sridingtorriger");
                ReSetAnime("groundtorriger");
                isSriding = true;
                sridingTime = 0;
                return;
            }
            animeState = "ground";
            SetAnime("groundtorriger");
            ReSetAnime("sridingtorriger");
            isSriding = false;
        }
    }
    //接触したらジャンプができる。ground。
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

            isjump = false;
            isdoublejump = false;
            rb2d.gravityScale = 1f;
            return;
        }
        if(jumpcount != 0&&Input.GetKey(KeyCode.DownArrow))//keycodeに変更
        {
            animeState = "sriding";
            SetAnime("sridingtorriger");
            ReSetAnime("jumptorriger");
            ReSetAnime("doublejumptorriger");

            audio.PlayOneShot(seSliding);
            isSriding = true;
            isjump = false;
            isdoublejump = false;
            jumpcount = 0;
            sridingTime = 0;

            rb2d.gravityScale = 1f;
            return;
        }
        if (isOkkake)
        {
            rb2d.gravityScale = 1f;
            animeState = "okkake";
            SetAnime("senpai");
            ReSetAnime("sridingtorriger");
            ReSetAnime("jumptorriger");
            ReSetAnime("doublejumptorriger");
            isjump = false;
            isdoublejump = false;
            jumpcount = 0;
        }
        rb2d.gravityScale = 1f;
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
        if (isSukebo||isStop) return;
        audio.PlayOneShot(seAccid);
        isStop = true;
        isjump = false;
        isSriding = false;
        Debug.Log("障害物に当たり申した");
        if (collision.gameObject.GetComponent<GimmickManager>().gimmick == GimmickKind.car)
        {
            Debug.Log("車に当たった");
            count = 0;
            speed = -0.1f;
            SetAnime("stop");
        }
        else if (collision.gameObject.GetComponent<GimmickManager>().gimmick == GimmickKind.dust)
        {
            Debug.Log("ごみに当たった");
            count = 0;
            speed = -0.1f;
            SetAnime("gomi");
        }
        else if (collision.gameObject.GetComponent<GimmickManager>().gimmick == GimmickKind.bat)
        {
            Debug.Log("蝙蝠に当たった");
            count = 0;
            speed = -0.1f;
            SetAnime("bat");
        }
        jumpcount = 0;
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
        Cut.SukeboCutIn();
        obj = Instantiate(sukeboEffect);
        obj.transform.parent = gameObject.transform;
        obj.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0f);
        isSukebo = true;
        GetComponent<Animator>().SetBool("sukeboBool", isSukebo);
        SetAnime("sukebo");
        ReSetAnime("groundtorriger");
        ReSetAnime("sridingtorriger");
        ReSetAnime("jumptorriger");
    }
    public void SukeboMove()
    {
        isSukebo = true;
        sukeboCount = 0;
        backSpeed.SpeedChange(0.1f);
    }
    private void pan()
    {
        audio.PlayOneShot(sePan);
        panCount++;
        //backSpeed.PanSpeedUp();
        Score.panScore(panCount);
        //ブーストゲージ
        if(boostState < 4)
        {
            boostState++;
            return;
        }
    }
    private void GageManeger(bool plus)
    {
        if (plus)
        {
            switch (boostState)
            {
                case 1:
                    //ゲージ１を伸ばす
                    slider[0].value += 1f*Time.deltaTime;
                    if (slider[0].value <= 2) return;
                    break;
                case 2:
                    //ゲージ２を伸ばす
                    slider[0].value += 1f * Time.deltaTime;
                    slider[1].value += 1f * Time.deltaTime;
                    if (slider[0].value <= 2) return;
                    break;
                case 3:
                    //ゲージ３を伸ばす
                    slider[0].value += 1f * Time.deltaTime;
                    slider[1].value += 1f * Time.deltaTime;
                    slider[2].value += 1f * Time.deltaTime;
                    if (slider[0].value <= 2) return;
                    break;
                default:
                    break;
            }
        }
        else
        {

            switch (boostState)
            {
                case 0:
                    //ゲージ123を減らす
                    slider[0].value -= 1f * Time.deltaTime;
                    slider[1].value -= 1f * Time.deltaTime;
                    slider[2].value -= 1f * Time.deltaTime;
                    if (gageMator[0] <= 0) return;
                    break;
                case 1:
                    //ゲージ２３を減らす
                    slider[1].value -= 1f * Time.deltaTime;
                    slider[2].value -= 1f * Time.deltaTime;
                    if (gageMator[1] <= 0) return;
                    break;
                case 2:
                    //ゲージ３を伸ばす
                    slider[2].value -= 1f * Time.deltaTime;
                    if (gageMator[2] <= 0) return;
                    break;
                default:
                    break;
            }
        }

    }
    /// <summary>
    /// ボタンを押したらスピードアップ
    /// </summary>
    private void PanDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))//keycodeに変更
        {
            GageManeger(false);
            if (boostState > 0)
            {
                if(animeState == "ground")
                {
                    SetAnime("dash");
                    ReSetAnime("groundtrriger");
                }
                GameObject panDash = Instantiate(panDashEffect);
                panDash.transform.parent = transform;
                panDash.transform.position = new Vector3(-2.13f - 4.3f, gameObject.transform.position.y-0.2f, 0f);
                audio.PlayOneShot(seDash);
                backSpeed.PanSpeedUp();
                boostState--;
                return;
            }
        }
    }
    public int panReturn()
    {
        return panCount;
    }
    //先輩にぶつかったとき
    public void InGorl()
    {
        SetAnime("end");
        isGorl = true;
    }
    public bool IsJump()
    {
        return isjump;
    }
    public void okkake()
    {
        SetAnime("senpai");
        
    }
    //ゲームオーバー
    private void InTime()
    {
        SetAnime("gameover");
        ReSetAnime("groundtorriger");
        ReSetAnime("sridingtorriger");
        ReSetAnime("jumptorriger");
        ReSetAnime("doublejumptorriger");
        ReSetAnime("sukebo");
        ReSetAnime("sukeboJump");
        isTime = true;
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
