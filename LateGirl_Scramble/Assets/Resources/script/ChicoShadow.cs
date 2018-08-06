using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChicoShadow : MonoBehaviour {

    [SerializeField]
    private GameObject chicoObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(chicoObj.transform.position.x, transform.position.y, transform.position.z);
	}
}
