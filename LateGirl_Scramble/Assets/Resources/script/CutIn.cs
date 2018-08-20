using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutIn : MonoBehaviour {

    public static bool isCutIn;
    private float cutinCount;
    [SerializeField]
    player Player;
    [SerializeField]
    EffectSpawn effect;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (isCutIn)
        {
            cutinCount += Time.deltaTime;
            if(cutinCount >= 1.5f)
            {
                Player.SukeboMove();
                gameObject.SetActive(false);
                GetComponent<Animator>().ResetTrigger("CutIn");
                isCutIn = false;
            }

        }
	}
    public void SukeboCutIn()
    {
        gameObject.SetActive(true);
        effect.Spawn();
        Debug.Log("スケボー");
        isCutIn = true;
        CutInPre();
    }
    private void CutInPre()
    {
        GetComponent<Animator>().SetTrigger("CutIn");
        cutinCount = 0;
    }
}
