using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBg : MonoBehaviour
{
    bool isEnd=false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!CountDown.isStart || CutIn.isCutIn|| isEnd) return;
        if (Gorl.isGorl && !isEnd)
        {
            isEnd = true;
        }
        transform.Translate(-0.1f / 4 * Time.deltaTime * 50f, 0, 0);
        if (transform.position.x <= -38.1f)
        {
            transform.position = new Vector3(36f + (97.2f - 94.42366f), 0f, 0);
        }
    }
}