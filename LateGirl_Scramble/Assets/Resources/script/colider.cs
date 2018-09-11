using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colider : MonoBehaviour
{
	public string nextScene = "trueSinarios";
    [SerializeField]
    private AudioSource _audios;
    [SerializeField]
    [Range(0, 1)]
    private float MVol = 1f;
    [SerializeField]
    bool push = false;
	private Gorl goal;
    [SerializeField]
    private float fadeAudio = 0.1f;
	void Update()
    {
		if (push == true)
        {
            _audios.volume = MVol -= fadeAudio;

            if (MVol <= 0f)
            {
                push = false;
            }
        }
    }
	// Use this for initialization
	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (goal.AfterGoal == 1)
		{
			//ここからリザルト遷移
			if (collision.gameObject.tag == "Player" && goal.AfterGoal == 1)
			{


				Debug.Log("壁に当たった");
				push = true;
				SceneLoadManager.LoadScene(nextScene);
				return;
			}
		}
	}
}
