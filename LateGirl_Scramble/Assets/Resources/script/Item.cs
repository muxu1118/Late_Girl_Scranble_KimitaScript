using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    pan,
    sukebo,
}

public class Item : MonoBehaviour {
    
    private Player player;
    public ItemType itemName;//アイテムの名前を入れる箱

    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーだったら破壊
        if (collision.gameObject.tag != "Player") return;
		gameObject.SetActive(false);
    }
    //アイテムの名前を呼び出す
    public int HeyItemName() {

        if (itemName == ItemType.pan)
        {
            return 1;
        }
        else if (itemName == ItemType.sukebo)
        {
            return 2;
        }
        return 0;
    }

}

