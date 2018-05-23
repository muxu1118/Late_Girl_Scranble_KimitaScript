using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {
	[SerializeField]
	private AsyncOperation async;
	[SerializeField]
	private GameObject loadUI;
	[SerializeField]
	private Slider slider;
	public void NextScene(){
		loadUI.SetActive(true);
		StartCoroutine("LoadData");
	}
	IEnumerator LoadData(){
		async = SceneManager.LoadSceneAsync("Load1");
		while(!async.isDone){
			var progressVal = Mathf.Clamp01(async.progress / 0.9f);
			slider.value = progressVal;
			yield return null;
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
