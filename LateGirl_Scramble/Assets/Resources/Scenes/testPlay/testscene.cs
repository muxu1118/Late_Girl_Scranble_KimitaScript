using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testscene : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		Invoke("scene", 7f);
	}

	// Update is called once per frame
	void Update()
	{

	}
	private void scene()
	{
		SceneLoadManager.LoadScene("StartScene");
	}
}
