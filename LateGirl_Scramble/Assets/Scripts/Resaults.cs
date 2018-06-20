using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resaults : MonoBehaviour {
	public string nextScene = "StartScene";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Onclic(){
		SceneLoadManager.LoadScene(nextScene);
	}
}
