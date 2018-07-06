using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscQuit : MonoBehaviour
{

	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
			return;
		}
		if (Input.GetKey(KeyCode.F2))
        {
			SceneManager.LoadScene("Init");
            return;
        }
	}
}
