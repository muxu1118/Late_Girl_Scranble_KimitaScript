using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class Gorl : MonoBehaviour {

    [SerializeField]
    private Timer timer;
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
    bool isGorl = false;
    [SerializeField]
    private float fadeAudio = 0.1f;

    // Use this for initialization
    void Start () {
        isGorl = false;
        //最初はラベルを表示させない
        GorlLabelObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //ゲームが終了したときラベルを表示させる
        if (timer.Count <= timer.CountLimit)
        {
            GorlLabelObject.SetActive(true);
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
            SceneLoadManager.LoadScene(nextScene);
            isGorl = false;
        }
	}

    public void InGorl()
    {
        isGorl = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ここからリザルト遷移
        if (collision.gameObject.tag == "Player")
        {
			push = true;
            Debug.Log("壁に当たった");
            isGorl = true;
        }
    }
}
