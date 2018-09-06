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
    [SerializeField]
    private AudioSource _audios;
    [SerializeField]
    private bool push = false;
    [SerializeField]
    private float MVol=1f;
    [SerializeField]
    private float fadeAudio=0.01f;


	// Use this for initialization
	void Start () {
		image.sprite = SinarioSprite[0];
	}
	
	// Update is called once per frame
	void Update () {
        if (push)
        {
            //SceneLoadManager.LoadScene(nextScene);
            _audios.volume = MVol -= fadeAudio;

            if (MVol <= 0f)
            {
                push = false;
            }
        }
		if (!flag)
		{
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.Return))
			{
				num++;
				if (num < SinarioSprite.Count)
				{

					image.sprite = SinarioSprite[num];
					Debug.Log(num);

				}
				else
				{
                    push = true;
					flag = true;
					SceneLoadManager.LoadScene(nextscene);
				}

			}
		}

	}
}
