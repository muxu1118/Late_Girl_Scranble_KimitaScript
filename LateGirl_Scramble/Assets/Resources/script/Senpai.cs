using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Senpai : MonoBehaviour
{
    [SerializeField]
    private player player;
    [SerializeField]
    private Gorl gorl;
    [SerializeField]
    private GameObject hukidashi;
    private bool isGorl;
    float time = 0.0f;
    // Use this for initialization
    void Start()
    {
        hukidashi.SetActive(false);
        isGorl = false;
        Debug.Log("iti" + transform.position.x);
        Debug.Log("time" + time);
        //約十秒で大体14.349936,14.949826,17.399738,17.49973
    }

    // Update is called once per frame
    void Update()
    {
        if (!CountDown.isStart || CutIn.isCutIn) return;
		if (!isGorl)transform.Translate(-0.05f * Time.deltaTime * 50f, 0, 0);
        else {
            time += Time.deltaTime;
        }
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.tag == "Player")
        {
            gorl.InGorl();
            player.InGorl();
            isGorl = true;
            hukidashi.SetActive(false);
            GetComponent<Animator>().SetTrigger("SenpaiGorl");
            Debug.Log("TriggerCollision");
        }
    }
}