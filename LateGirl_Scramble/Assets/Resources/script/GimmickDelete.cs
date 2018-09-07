using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickDelete : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Block"|| collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block"|| collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
        }
    }
}
