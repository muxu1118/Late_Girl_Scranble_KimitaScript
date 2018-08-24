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
    [SerializeField]
    private Gorl gorl;
    private static int panIntCount = 0;
    [SerializeField]
    private GameObject[] pan = new GameObject[10];
    [SerializeField]
    private GameObject[] sukebo = new GameObject[5];
    [SerializeField]
    GameObject[] pattern = new GameObject[4];
	[SerializeField]
	GameObject itemPosition;
    GameObject ParentObject;
    private warp[] warpScript = new warp[4];
    public static bool isChange = false;
    public static bool isCount = false;
    private int rand;
	private const int scrollLimit = 10;
    [SerializeField]
    private float yDebug=0F;
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
			if(i < 5){
				sukebo[i].SetActive(true);
			}
        }
		panIntCount = 0;
        isChange = true;

    }
    //スピードをプレイヤーから持ってこれるようpublic
    
    void Update () {
        if (!CountDown.isStart||CutIn.isCutIn) return;
        //時間の間地面を移動させる
        if (!gorl.IsGorl())
        {
            transform.Translate(BackSceneSpeed * Time.deltaTime*50f, 0, 0);
            if (isChange)
            {
                Change();
				ItemLayout();
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
            // スプライトの幅を取得
            //float width = GetComponent<SpriteRenderer>().bounds.size.x;
          
            ParentObject = Instantiate(pattern[rand], new Vector3(30f, yDebug, 0), Quaternion.identity);

            ParentObject.transform.parent = transform;
            // 幅2個数分だけ右へ移動
            ParentObject.transform.position = new Vector3(37.3f, yDebug, 0);
            //pattern[rand].transform.position = new Vector3(44.8f, 12.7f-1.0f, 0);
            warpScript[rand].EndFalse();
            isChange = false;
            return;
        }
    }
	private void ItemLayout(){
		if (panIntCount % 2 == 0 && panIntCount % 2 <= scrollLimit&&isCount)
        {
			Debug.Log("パン屋");
			pan[panIntCount / 2].transform.position = new Vector3(itemPosition.transform.position.x, Random.Range(-4f, 0), 0);
            if (panIntCount % 4 == 0 && panIntCount % 4 <= scrollLimit / 2)
            {
				Debug.Log("なんでやねん");
				sukebo[panIntCount / 4].transform.position = new Vector3(itemPosition.transform.position.x- 37.3f, Random.Range(-4f, 0), 0);
            }
			isCount = false;
        }
        /*else if (panIntCount == 1)
        {
			pan[panIntCount - 1].transform.position = new Vector3(itemPosition.transform.position.x- 30.8f, Random.Range(-4f, 0), 0);
			isCount = false;
		}*/
	}
    public void Moving()
    {
        isChange = true;
		isCount = true;
    }
}
