using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    //ゲームタイム
    [SerializeField]
    private GameObject showSprite;
    [SerializeField]
    private Image[] numberImages;
    [SerializeField]
    private static float count = 120f;
    private static bool stop = false;
    /// <summary>
    ///ゲームの終了する時間
    /// </summary>
    [SerializeField]
    private float countLimit = 0f;
    public float num;
    public int digit = 16;
    public bool zeroFill = false;
    private Vector3 timePosition;
    private List<Image> NumImageList = new List<Image>();
    private GameObject timesprite;
    private GameObject[] numSpriteGird;         // 表示用スプライトオブジェクトの配列
    private Dictionary<char, Sprite> dicSprite; // スプライトディクショナリ

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
    }

    // Update is called once per frame
    void Update()
    {

        if (count > countLimit&&!stop)
        {
            count -= Time.deltaTime; //スタートしてからの秒数を格納
            timerSet();


        }else
        {
            stop = true;
        }
        if(stop)
        {
            timerSet();
        }


    }
    private void timerSet()
    {
        //int limit = (int)countLimit;
        int minute = (int)count / 60;
        float seconds = count - minute * 60;
        numberImages[0].sprite = spriteNumbers[minute];//他にもTimerScriptの付いているオブジェクトがあって、そちらでnumberImagesが設定されていなかったのがバグの原因でした
        numberImages[1].sprite = spriteNumbers[(int)seconds / 10];
        numberImages[2].sprite = spriteNumbers[(int)seconds % 10];
        numberImages[3].sprite = spriteNumbers[(int)(seconds * 10 % 10)];


        /*
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
    }
    public void gorlGone(bool isGorl)
    {
        stop = isGorl;
    }
} 
