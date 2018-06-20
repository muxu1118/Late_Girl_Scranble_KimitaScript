using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour {

	bool onLoad = false;

    void Update()
    {
        if (!Application.isShowingSplashScreen && !onLoad)
        {
            onLoad = true;
            SceneLoadManager.LoadScene("StartScene");
        }
    }

}
