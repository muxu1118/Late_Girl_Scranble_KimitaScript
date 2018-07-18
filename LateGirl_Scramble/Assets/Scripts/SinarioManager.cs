using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinarioManager : MonoBehaviour {
	[SerializeField]
	private string nextscene = "mainScene";
	[SerializeField]
	private List<Sprite> SinarioSprite = new List<Sprite>();
	[SerializeField]
	int num;
	[SerializeField]
	private Image image;
	[SerializeField]
	private bool flag = false;
	// Use this for initialization
	void Start () {
		image.sprite = SinarioSprite[0];
	}
	
	// Update is called once per frame
	void Update () {
		if (flag == false)
		{
			if (Input.GetMouseButtonDown(0) || Input.GetKey("space"))
			{
				num++;
				if (num < SinarioSprite.Count)
				{

					image.sprite = SinarioSprite[num];
					Debug.Log(num);

				}
				else
				{
					flag = true;
					SceneLoadManager.LoadScene(nextscene);
				}

			}
		}

	}
}
