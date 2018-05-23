using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickManager : MonoBehaviour {
    Vector3 gimmickPosition;
    [SerializeField]
    private Timer time;
    // Use this for initialization
    void Start () {
        gimmickPosition = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-0.1f, 0, 0);
        if (transform.position.x < (- 10))
        {
            transform.position = new Vector3(25f, gimmickPosition.y, 0);
        
        }
        /*if(time.Count <= time.CountLimit)
        {
            Destroy(gameObject);
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //どうしようか
    }
}
