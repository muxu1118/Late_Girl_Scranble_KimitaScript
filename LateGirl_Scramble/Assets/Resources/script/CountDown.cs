using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

    private int countDown = 3;
    public static bool isStart;
    [SerializeField]
    Image countDownImage;
    [SerializeField]
    Sprite[] countDownSprite;

	void Start () {
        isStart = false;
        StartCoroutine(TimeDown(countDown,1f));
        StartCoroutine(SpriteChange(5f));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private IEnumerator TimeDown(int time, float wait)
    {
        float nowTime = 0;
        while (time > nowTime)
        {

            countDownImage.sprite = countDownSprite[(int)nowTime];
            yield return new WaitForSeconds(wait);
            countDownImage.sprite = countDownSprite[(int)nowTime];
            nowTime ++;
            //yield return StartCoroutine(Waiting(1.0f));
        }
        if (nowTime >= time)
        {
            isStart = true;
            countDownImage.sprite = countDownSprite[3];
        }
    }
    private IEnumerator SpriteChange(float wait)
    {
        yield return new WaitForSeconds(wait);
        gameObject.SetActive(false);
    }
    private IEnumerator Waiting(float value)
    {
        yield return new WaitForSeconds(value);
    }
}
