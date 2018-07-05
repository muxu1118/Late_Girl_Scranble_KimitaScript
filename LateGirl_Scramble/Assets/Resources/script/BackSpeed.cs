using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSpeed : MonoBehaviour {

    private float BackSceneSpeed = 0f;
    private float panSpeed = 0f;
    [SerializeField]
    private Timer time;
    [SerializeField]
    private float timeCount=0f;

    public float BSpeed
    {
        get{
            return BackSceneSpeed;
        }
    }
    public float PanSpeed
    {
        get
        {
            return panSpeed;
        }
    }
    public void SpeedChange(float speed)
    {
        BackSceneSpeed = speed;
    }
    
    //パンのスピードアップ
    public void PanSpeedUp()
    {
        panSpeed = 0.1f;
        timeCount = 0;
        
    }
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if (time.Count > time.CountLimit)
        {
            transform.Translate(-BackSceneSpeed-panSpeed, 0, 0);

            if (panSpeed > 0.0f)
            {
                panSpeed -= timeCount/2;
            }
            else
            {
                panSpeed = 0f;
            }
            timeCount += Time.deltaTime/1000;
        }

        
    }
}
