using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        transform.Translate(-0.1f, 0, 0);
        if (transform.position.x < -10.65f*2)
        {
            transform.position = new Vector3(10.65f * 2, -1, 0);
        }
    }
}
