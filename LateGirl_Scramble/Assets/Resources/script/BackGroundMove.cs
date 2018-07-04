using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour {
    //バックグラウンドのスピードとプレイヤーのスピードを一緒にしたい（したい）
    // Update is called once per frame
    [SerializeField] //←SerializeFieldにする事でprivateでもUnityのinspectorに表示される
    private float BackSceneSpeed = -0.1f;
    [SerializeField]
    private Timer time;
    [SerializeField]
    GameObject[] pattern = new GameObject[4];
    warp[] warpScript = new warp[4];
    private bool isChange=false;
    private int rand;
    public float BackSpeed
    {
        get
        {
            return BackSceneSpeed;
        }
        set
        {
            BackSceneSpeed = value;
        }
    }
    void Start()
    {
        for(int i = 0;i < warpScript.Length; i++)
        {
            warpScript[i] = pattern[i].gameObject.GetComponent<warp>();
        }
        isChange = true;

    }
    //スピードをプレイヤーから持ってこれるようpublic
    
    void Update () {
        //時間の間地面を移動させる
        if (time.Count > time.CountLimit)
        {
            transform.Translate(BackSceneSpeed, 0, 0);
            if (isChange)
            {
                Change();
            }
        }
        else
        {
            BackSceneSpeed = 0;
        }
    }
    private void Change()
    {
        rand = Random.Range(0, 4);
        if (warpScript[rand].IsEnd())
        {
            pattern[rand].transform.position = new Vector3(44.8f, 12.7f-1.0f, 0);
            warpScript[rand].EndFalse();
            isChange = false;
            return;
        }
    }
    public void Moving()
    {
        isChange = true;
    }
}
