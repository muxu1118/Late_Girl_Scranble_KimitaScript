﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour {

    [SerializeField]
    private float speed = 0.1f;

    // Update is called once per frame
    void Update () {
        if (!CountDown.isStart || CutIn.isCutIn)return;
        transform.Translate(-1 * speed * Time.deltaTime * 50f, 0, 0);
    }
}
