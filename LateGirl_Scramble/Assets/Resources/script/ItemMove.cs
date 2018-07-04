using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour {

    [SerializeField]
    private float speed = 0.1f;

    // Update is called once per frame
    void Update () {

        transform.Translate(-1 * speed, 0, 0);
    }
}
