using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hukidashi : MonoBehaviour {
    [SerializeField]
    private GameObject huki;//吹き出し

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Senpai")
        {
            huki.SetActive(true);
        }
    }
}
