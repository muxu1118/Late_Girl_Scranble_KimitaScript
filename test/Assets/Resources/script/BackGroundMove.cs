﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour {
    // Update is called once per frame
    [SerializeField] //←SerializeFieldにする事でprivateでもUnityのinspectorに表示される
    private float BackSceneSpeed = -0.1f;
    [SerializeField]
    private Timer time;
	void Update () {
        //時間の間地面を移動させる
        if (time.Count < time.CountLimit - 0.01)
        {
            transform.Translate(BackSceneSpeed, 0, 0);
            if (transform.position.x < -10.65f * 2)
            {
                transform.position = new Vector3(10.65f * 2, -1, 0);
            }
        }
        
    }
}
