using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    //ゲームタイム
    private float count = 0;
    /// <summary>
    ///ゲームの終了する時間
    /// </summary>
    [SerializeField]
    private float countLimit = 300;
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

    // Update is called once per frame
    void Update () {
        if (count < countLimit-0.01)
        {
            count += Time.deltaTime; //スタートしてからの秒数を格納
            GetComponent<Text>().text = count.ToString("F2"); //小数2桁にして表示
        }
    }
}
