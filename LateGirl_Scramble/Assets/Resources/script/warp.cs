using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warp : MonoBehaviour {

    private bool isEnd = false;
    [SerializeField]
    private BackGroundMove BGM;

    private void Update()
    {
        if (transform.position.x <= -31.61f && !isEnd)
        {
            isEnd = true;
            BGM.Moving();
        }

    }
    public bool IsEnd()
    {
        return isEnd;
    }

    public void EndFalse()
    {
        isEnd = false;
    }
}
