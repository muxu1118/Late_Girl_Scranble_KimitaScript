using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    //ゲームタイム
    [SerializeField]
    private float count = 0f;
    /// <summary>
    ///ゲームの終了する時間
    /// </summary>
    [SerializeField]
    private float countLimit = 300f;
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

    private void Awake()
    {
        Debug.Log(GetComponent<Text>().fontSize);
    }

    // Update is called once per frame
    void Update () {
        if (count < countLimit)
        {
            count += Time.deltaTime; //スタートしてからの秒数を格納
            GetComponent<Text>().text = count.ToString("F2"); //小数2桁にして表示
        }
        else
        {
			count = countLimit;
            GetComponent<Text>().text = count.ToString("F2");
        }

        
    }
}
