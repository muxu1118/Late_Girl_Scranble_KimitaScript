using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Senpai : MonoBehaviour
{
    [SerializeField]
    private player player;
    [SerializeField]
    private Gorl gorl;

    float time = 0.0f;
    // Use this for initialization
    void Start()
    {

        Debug.Log("iti" + transform.position.x);
        Debug.Log("time" + time);
        //約十秒で大体14.349936,14.949826,17.399738,17.49973
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= 10)
        {
            Debug.Log("iti" + transform.position.x);
            Debug.Log("time" + time);
            time -= 10.0f;
        }
        time += Time.deltaTime;
        transform.Translate(-0.05f, 0, 0);

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.InGorl();
            Debug.Log("普通のcollision");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            player.InGorl();
            gorl.InGorl();
            Debug.Log("TriggerCollision");
        }
    }
}