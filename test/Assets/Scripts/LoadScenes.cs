using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingScenes : MonoBehaviour {

	[SerializeField]
	private AsyncOperation async;
	public GameObject LoadingUI;
	public Slider Slider;
	public void LoadNextScene(){
		LoadingUI.SetActive(true);
		StartCoroutine(LoadScene());
	}

	IEnumerator LoadScene(){
		async = SceneManager.LoadSceneAsync("");
		while(!async.isDone){
			Slider.value = async.progress;
			yield return null;
		}
	}
}
