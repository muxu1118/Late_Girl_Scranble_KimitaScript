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

    [SerializeField]
    private float fadeAudio = 0.1f;

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
			push = true;
        }

		if (push == true)
		{
			_audios.volume = MVol -= fadeAudio;

			if (MVol <= 0f)
			{
				push = false;
			}
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ここからリザルト遷移
        if (collision.gameObject.tag == "Player")
        {
			push = true;
            Debug.Log("壁に当たった");

			SceneLoadManager.LoadScene(nextScene);
        }
    }
}
