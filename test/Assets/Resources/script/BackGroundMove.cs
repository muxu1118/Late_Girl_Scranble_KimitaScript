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

    void Update () {
        //時間の間地面を移動させる
        if (time.Count <= time.CountLimit)
        {
            transform.Translate(BackSceneSpeed, 0, 0);
            if (transform.position.x < (-21.2 - 4.37))
            {
                transform.position = new Vector3(21.2f + 4.37f , 0.21f, 0);
            }
        }
        else
        {
            BackSceneSpeed = 0;
        }
        
    }
}
