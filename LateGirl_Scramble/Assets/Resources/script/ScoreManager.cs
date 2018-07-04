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
    public static int pan=0;
    [SerializeField]
    private Image[] numberImages;
    private player player;
    
    [SerializeField]
    private Sprite[] spriteNumbers = new Sprite[10];
    // Use this for initialization
    void Start() {
        numberImages[0].sprite = spriteNumbers[pan / 10];
        numberImages[1].sprite = spriteNumbers[pan % 10];
    }	
    public void panScore(int panCount)
    {
        pan = panCount;
    }
	// Update is called once per frame
	void Update () {
        
    }
}
