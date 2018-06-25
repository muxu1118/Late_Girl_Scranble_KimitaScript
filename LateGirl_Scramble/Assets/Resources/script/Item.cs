using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    
    private player player;
    public string itemName;//アイテムの名前を入れる箱

    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーだったら破壊
        if (collision.gameObject.tag != "Player") return;
        Destroy(gameObject);
    }
    //アイテムの名前を呼び出す
    public int HeyItemName() {
        itemName = gameObject.name;
        Debug.Log(gameObject.name);
        if (itemName == "pan")
        {
            Debug.Log("pan");
            return 1;
        }
        else if (itemName == "sukebo")
        {
            return 2;
        }
        return 0;
    }

}

