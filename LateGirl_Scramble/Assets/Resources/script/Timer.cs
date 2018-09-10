using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    // ゲームタイム
    [SerializeField]
    private Image[] numberImages;
    // リザルト時のスコア
    [SerializeField]
    private Sprite[] scoreImages;

    // ゲーム内タイム。シーン別でも変わらないようにstaticつけてます。
    private static float count = 120f;
    // ゲームが時間で終わったかどうかのbool
    private static bool stop = false;
    /// <summary>
    ///ゲームの終了する時間
    /// </summary>
    [SerializeField]
    private float countLimit = 0f;
    // 数字のスプライト
    [SerializeField]
    private Sprite[] spriteNumbers = new Sprite[10];
   
    //ほかのスクリプトから呼び出せるように
    public float Count
    {
        get
        {
            return count;
        }
    }
    //同じく呼び出せるように
    public float CountLimit
    {
        get
        {
            return countLimit;
        }
    }

    void Start()
    {
        if (!stop) count = 120f;
        Debug.Log((int)count / 60);
        // スタートは2:00.0からスタート
        numberImages[0].sprite = spriteNumbers[2];
        numberImages[1].sprite = spriteNumbers[0];
        numberImages[2].sprite = spriteNumbers[0];
        numberImages[3].sprite = spriteNumbers[0];
    }

    // Update is called once per frame
    void Update()
    {
        // カットイン、カウントダウンしているときには数えない
        if (!CountDown.isStart||CutIn.isCutIn) return;
        // ゲームが終了していないときに
        if (count > countLimit&&!stop)
        {
            count -= Time.deltaTime; //timeを減らす
            timerSet(); // ゲーム時間をイメージにつける
        }else
        {
            // 時間がゲーム終了時間になったらstopをtrueに
            stop = true;
        }
        if(stop)
        {
            timerSet();// 同じく
            numberImages[4].sprite = scoreImages[Hako()];//リザルトの点数
        }


    }
    
    /// <summary>
    /// Countを時間に変換しその時間をイメージにつける
    /// </summary>
    private void timerSet()
    {
        //　分に直す
        int minute = (int)count / 60;
        //　秒に直す
        float seconds = count - minute * 60;

        numberImages[0].sprite = spriteNumbers[minute];                     // 分
        numberImages[1].sprite = spriteNumbers[(int)seconds / 10];          // 二桁秒
        numberImages[2].sprite = spriteNumbers[(int)seconds % 10];          // 一桁秒
        numberImages[3].sprite = spriteNumbers[(int)(seconds * 10 % 10)];   // コンマ一桁
        numberImages[4].sprite = scoreImages[Hako()];                       // リザルトのスコア
        
    }
    // 時間でゲームオーバーになったら別のスクリプトに飛ばせるように(ほかの方法もあるかも)
    public void gorlGone(bool isGorl)
    {
        stop = isGorl;
    }
    // リザルトの点数計算（簡易）
    private int Hako()
    {
        // 花丸
        if (count >= 30f) return 0;
        else if (count > 20f) // 80点
            return 1;
        else if (count > 10f)   // 50点
            return 2;
        else　　　　　          // 0点
            return 3;

    }
}
/*
 * 使わなかったスクリプト
 * 
    [SerializeField]
    private GameObject showSprite;
private void Awake()
{

    dicSprite = new Dictionary<char, Sprite>() {
        { '0', spriteNumbers[0] },
        { '1', spriteNumbers[1] },
        { '2', spriteNumbers[2] },
        { '3', spriteNumbers[3] },
        { '4', spriteNumbers[4] },
        { '5', spriteNumbers[5] },
        { '6', spriteNumbers[6] },
        { '7', spriteNumbers[7] },
        { '8', spriteNumbers[8] },
        { '9', spriteNumbers[9] },
    };
}
    var numSprite = GetComponent<time>();
    numSprite.Value = (int)count; // ここで「1234」の値を指定

    GetComponent<SpriteRenderer>().sprite = spriteNumbers[(int)time%10];


    // 表示文字列取得
    string strValue = count.ToString();

    // 現在表示中のオブジェクト削除
    //if (numSpriteGird != null)
    //{
    //    foreach (var numSprite in numSpriteGird)
    //    {
    //        GameObject.Destroy(numSprite);
    //    }
    //}

    // 表示桁数分だけオブジェクト作成
    numSpriteGird = new GameObject[strValue.Length];
    for (var i = 0; i < numSpriteGird.Length; ++i)
    {
        // オブジェクト作成
        numSpriteGird[i] = Instantiate(
            showSprite,
            transform.position + new Vector3((float)i*2, 0),
            Quaternion.identity) as GameObject;

        // 表示する数値指定
        numSpriteGird[i].GetComponent<SpriteRenderer>().sprite = dicSprite[strValue[i]];

        // 自身の子階層に移動
        numSpriteGird[i].transform.parent = transform;
    }
    */
