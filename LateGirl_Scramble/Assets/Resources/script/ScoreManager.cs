using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    /*
    public static ScoreManager instance;
    public int score;

    private void Awake()
    {
        if (instance) { Destroy(this); }
        instance = this;
    }
    */
    [SerializeField]
    private Image[] numberImages;
    private player player;
    [SerializeField]
    private Sprite[] spriteNumbers = new Sprite[10];
    // Use this for initialization
    void Start () {
        /*player = GetComponent<player>();
        numberImages[0].sprite = spriteNumbers[player.panReturn() / 10];//他にもTimerScriptの付いているオブジェクトがあって、そちらでnumberImagesが設定されていなかったのがバグの原因でした
        numberImages[1].sprite = spriteNumbers[player.panReturn() % 10];*/
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
