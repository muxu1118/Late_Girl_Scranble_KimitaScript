using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscQuit : MonoBehaviour
{

	void Update()
	{
		if (Input.GetKey(KeyCode.F5))
		{
			Application.Quit();
			return;
		}
        if (Input.GetKey(KeyCode.F6))
        {
			SceneManager.LoadScene("Init");
            return;
        }
	}
}
