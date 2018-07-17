using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class buttonanimation : MonoBehaviour {
    
	public string nextScene = "MainScene";
	[SerializeField]
    RectTransform rectTran;
	[SerializeField]
    private AudioSource _audios;
	[SerializeField]
	[Range (0,1)]
	private float MVol = 1f;
	[SerializeField]
	bool push = false;

	[SerializeField]
	private float fadeAudio=0.1f;
	[SerializeField]
	private AudioSource _Clicks;
	// Use this for initialization
	void Start () {
		DOTween.Init();
		rectTran.DOScale(new Vector3(2.3f, 2.3f, 2.3f), 0.8f)
                .SetEase(Ease.Linear)
		        .SetLoops(-1, LoopType.Yoyo
                         );
	}

	// Update is called once per frame
	void Update()
	{
		if (push == true)
		{
			//SceneLoadManager.LoadScene(nextScene);
			_audios.volume = MVol -= fadeAudio;
            
			if (MVol <= 0f)
            {
                push = false;
            }
		}

		if(Input.GetKey("space")){
			StartPush();
		}
	}

	public void StartPush(){
		push = true;
		_Clicks.Play();
		SceneLoadManager.LoadScene(nextScene);
	}
}
