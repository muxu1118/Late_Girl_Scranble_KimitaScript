using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resaults : MonoBehaviour {
	//public string nextScene = "StartScene";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void HomeButton(){
		SceneLoadManager.LoadScene("StartScene");
		return;
	}
	public void NextButton(){
		SceneLoadManager.LoadScene("nextscene");
		return;
	}
	public void ReStartButton(){
		SceneLoadManager.LoadScene("mainScene");
		return;
	}
}
