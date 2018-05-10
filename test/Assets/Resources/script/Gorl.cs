﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gorl : MonoBehaviour {

    [SerializeField]
    private Timer timer;
    //ゴール時に出る文字
    public GameObject GorlLabelObject;

    // Use this for initialization
    void Start () {
        //最初はラベルを表示させない
        GorlLabelObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //ゲームが終了したときラベルを表示させる
        if (timer.Count + 0.01 >= timer.CountLimit)
        {
            GorlLabelObject.SetActive(true);
        }
	}
}
