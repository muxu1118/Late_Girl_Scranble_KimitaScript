using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawn : MonoBehaviour {

    //　出現させるエフェクト
    [SerializeField] private GameObject effectObject;
    //　エフェクトを消す秒数
    [SerializeField] private float deleteTime;
    //　エフェクトの出現位置のオフセット値
    [SerializeField] private float offset;
    [SerializeField] GameObject canvas;
    // Use this for initialization
    void Start()
    {
        //　ゲームオブジェクト登場時にエフェクトをインスタンス化
        var instantiateEffect = GameObject.Instantiate(effectObject, transform.position + new Vector3(0f, offset, 0f), Quaternion.identity) as GameObject;
        Destroy(instantiateEffect, deleteTime);
    }

    public void Spawn()
    {
        var instantiateEffect = GameObject.Instantiate(effectObject, transform.position + new Vector3(-750f, 350f, 0f), Quaternion.identity) as GameObject;
        var instantiateEffect2 = GameObject.Instantiate(effectObject, transform.position + new Vector3(0f, -350f, 0f), Quaternion.identity) as GameObject;
        instantiateEffect.transform.parent = canvas.transform;
        instantiateEffect2.transform.parent = canvas.transform;
        Destroy(instantiateEffect, deleteTime);
        Destroy(instantiateEffect2, deleteTime);
    }

}
