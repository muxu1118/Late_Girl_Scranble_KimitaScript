using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject senpai;
    [SerializeField]
    private GameObject chicoMap;
    [SerializeField]
    private GameObject senpaiMap;
    [SerializeField]
    private BackSpeed BS;
    private float parsent = 0;
    bool isEnd=false;
    private void Update()
    {

        if (!CountDown.isStart||CutIn.isCutIn || isEnd) return;
        if (Gorl.isGorl && !isEnd)
        {
            isEnd = true;
        }
        chicoMap.transform.Translate((((0.05f / parsent) * 2  + Kasoku())* 0.795F) * Time.deltaTime * 50f, 0, 0);
		senpaiMap.transform.Translate(((0.05f / parsent)* 0.5F) * Time.deltaTime * 50f, 0, 0);
    }
    void Start()
    {
        isEnd = false;
        Speed();
    }
    private float Kasoku()
    {
        return (BS.BSpeed + BS.PanSpeed)  / parsent;
    }
    private void Speed()
    {
        parsent = (senpai.transform.position.x - player.transform.position.x) / (senpaiMap.transform.position.x - chicoMap.transform.position.x);
        
    }
}
