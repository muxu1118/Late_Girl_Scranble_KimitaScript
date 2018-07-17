using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class Gorl : MonoBehaviour {

    [SerializeField]
    private Timer timer;
    [SerializeField]
    private BackSpeed BS;
    public string nextScene="Resaults";
    //ゴール時に出る文字
    public GameObject GorlLabelObject;

	[SerializeField]
    private AudioSource _audios;
    [SerializeField]
    [Range(0, 1)]
    private float MVol = 1f;
    [SerializeField]
    bool push = false;
    public static bool isGorl = false;
    public static bool isOnce = false;
    [SerializeField]
    private float fadeAudio = 0.1f;
	//[SerializeField]
	public int aftergoal = 1;
    float gorlCount = 0;
    // Use this for initialization
    void Start () {
        isGorl = false;
		isOnce = false;
        //最初はラベルを表示させない
        GorlLabelObject.SetActive(false);
        gorlCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        //ゲームが終了したときラベルを表示させる
		if (timer.Count <= timer.CountLimit&&!isOnce)
        {
			isOnce = true;
            GorlLabelObject.SetActive(true);
			isGorl = true;
			push = true;
        }
        //音
		if (push == true)
		{
			_audios.volume = MVol -= fadeAudio;

			if (MVol <= 0f)
			{
				push = false;
			}
		}
        if (isGorl)
        {
			aftergoal = 0;
            gorlCount += Time.deltaTime;
            if (gorlCount >= 3f)
            {
                SceneLoadManager.LoadScene(nextScene);
                isGorl = false;
            }
        }
	}

	public bool InTime()
    {
		return isOnce;
    }
    public bool IsGorl()
    {
        return isGorl;
    }

    public void InGorl()
    {
        isGorl = true;
        BS.IsGorl();
        GorlLabelObject.SetActive(true);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ここからリザルト遷移
        if (collision.gameObject.tag == "Player")
        {

            GorlLabelObject.SetActive(true);
            push = true;
            Debug.Log("壁に当たった");
            isGorl = true;
        }
    }
	public int AfterGoal{
		get
		{
			return aftergoal;
		}
	}
}
