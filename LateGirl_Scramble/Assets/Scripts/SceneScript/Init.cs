using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour {

	bool onLoad = false;

	private void Awake()
	{
		Screen.SetResolution(1920, 1080, Screen.fullScreen);
	}

	void Update()
    {
        if (!Application.isShowingSplashScreen && !onLoad)
        {
            onLoad = true;
            SceneLoadManager.LoadScene("StartScene");
        }
    }

}
