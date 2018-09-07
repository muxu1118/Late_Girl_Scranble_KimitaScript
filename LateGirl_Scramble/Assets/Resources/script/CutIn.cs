using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutIn : MonoBehaviour {

    public static bool isCutIn; // カットインの間true
    private float cutinCount; // カットインを数える変数
    [SerializeField]
    player Player; // プレイヤーがスケボーを取ったら
    [SerializeField]
    EffectSpawn effect; // カットイン時に出すエフェクト 

    Animator anim;// アニメーター
    void Start () {
        //　publicだとGetComponentできないためanimに入れる
        anim = GetComponent<Animator>();
    }
	
	
	void Update () {
        // カットインしている間
        if (isCutIn)
        {
            // カットインをしている時間
            cutinCount += Time.deltaTime;
            // カットインが終わったら（リミット１．５秒）
            if(cutinCount >= 0.75f)
            {
                // プレイヤーをスケボー状態にする
                Player.SukeboMove();
                // カットインを見えなくする
                gameObject.SetActive(false);
                // アニメーターを止める
                GetComponent<Animator>().ResetTrigger("CutIn");
                // カットインを閉じる
                isCutIn = false;
            }
        }
	}
    // カットインを出す（playerがスケボーを取ったら）
    public void SukeboCutIn()
    {
        // 有効化
        gameObject.SetActive(true);
        // エフェクトを出す
        effect.Spawn();
        // カットイン状態
        isCutIn = true;
        // アニメーションを動かす
        anim.SetTrigger("CutIn");
        // カットインの時間を数える
        cutinCount = 0;
    }
}
