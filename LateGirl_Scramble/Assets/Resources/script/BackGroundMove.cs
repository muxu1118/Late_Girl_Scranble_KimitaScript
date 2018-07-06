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
    private static int panIntCount = 0;
    [SerializeField]
    private GameObject[] pan = new GameObject[10];
    [SerializeField]
    private GameObject[] sukebo = new GameObject[5];
    [SerializeField]
    GameObject[] pattern = new GameObject[4];
	private warp[] warpScript = new warp[4];
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
		for (int i = 0; i < pan.Length; i++)
        {
			pan[i].SetActive(true);
			if(i<5){
				sukebo[i].SetActive(true);
			}
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
                if (panIntCount % 2 == 0 && panIntCount % 2 <= 10)
                {
                    pan[panIntCount / 2].transform.position = new Vector3(64f, Random.Range(-4f, 0), 0);
                    if (panIntCount % 4 == 0 && panIntCount % 4 <= 5)
                    {
                        sukebo[panIntCount / 4].transform.position = new Vector3(22.8f, Random.Range(-4f, 0), 0);
                    }
                } else if (panIntCount == 1)
                {
                    pan[panIntCount - 1].transform.position = new Vector3(18.8f, Random.Range(-4f, 0), 0);
                }
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
            panIntCount++;
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
