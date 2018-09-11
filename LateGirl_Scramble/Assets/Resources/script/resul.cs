using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resul : MonoBehaviour {

    [SerializeField]
    Image gameEndImage;
    [SerializeField]
    Sprite[] gameEndSprite;
	// Use this for initialization
	void Start () {
        if (Gorl.isOnce)
        {
            gameEndImage.sprite = gameEndSprite[0];
        }else
        {
            gameEndImage.sprite = gameEndSprite[1];
        }
	}
}
