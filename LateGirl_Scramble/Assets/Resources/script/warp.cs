using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {
    
    private static bool isEnd = false;

    //private BackGroundMove BGM;
    private void Start()
    {
    }
    private void Update()
    {
        

        if (transform.position.x <= -38.1f && !isEnd)
        {
            isEnd = true;
            
            BackGroundMove.isChange = true;
            BackGroundMove.isCount = true;
            Destroy(gameObject);
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
