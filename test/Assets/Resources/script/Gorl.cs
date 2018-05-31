using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gorl : MonoBehaviour {

    [SerializeField]
    private Timer timer;
    //ゴール時に出る文字
    public GameObject GorlLabelObject;

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
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ここからリザルト遷移
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("壁に当たった");
			SceneManager.LoadScene("Resaults");
        }
    }
}
