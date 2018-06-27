using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour {
    //バックグラウンドのスピードとプレイヤーのスピードを一緒にしたい（したい）
    // Update is called once per frame
    [SerializeField] //←SerializeFieldにする事でprivateでもUnityのinspectorに表示される
    private float BackSceneSpeed = -0.1f;
    private float panSpeed = 0f;
    [SerializeField]
    private Timer time;

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
    //スピードをプレイヤーから持ってこれるようpublic
    public void SpeedChange(float speed)
    {
        BackSceneSpeed = speed;
    }
    //パンのスピードアップ
    public void PanSpeedUp()
    {
        panSpeed = 2.0f;
    }
    void Update () {
        //時間の間地面を移動させる
        if (time.Count > time.CountLimit)
        {
            transform.Translate(BackSceneSpeed+panSpeed, 0, 0);
            if (transform.position.x < -(31.5))
            {
                transform.position = new Vector3(21.2f + 4.37f , 0.21f, 0);
            }
            if (panSpeed > 0.0f)
            {
                panSpeed =- 0.02f;
            }
        }
        else
        {
            BackSceneSpeed = 0;
        }
        
    }
}
