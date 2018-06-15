using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colider : MonoBehaviour
{
	public string nextScene = "Resaults";
    [SerializeField]
    private AudioSource _audios;
    [SerializeField]
    [Range(0, 1)]
    private float MVol = 1f;
    [SerializeField]
    bool push = false;

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
		
		//ここからリザルト遷移
		if (collision.gameObject.tag == "Player")
		{
			

			Debug.Log("壁に当たった");
			push = true;
			SceneLoadManager.LoadScene(nextScene);
		}
	}
}
