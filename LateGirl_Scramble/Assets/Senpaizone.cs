using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Senpaizone : MonoBehaviour {


    [SerializeField]
    private GameObject hukidashi;

    Player player;
    // Use this for initialization
    void Start () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("なんで吹き出しでないん？");
            hukidashi.SetActive(true);
            player = collision.GetComponent<Player>();
            player.okkake();
        }
    }
}
