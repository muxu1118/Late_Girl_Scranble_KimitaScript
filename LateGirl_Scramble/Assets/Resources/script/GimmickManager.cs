using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickManager : MonoBehaviour {
    Vector3 gimmickPosition;
    [SerializeField]
    private Timer time;
	[SerializeField]
	private bool GimickBack = true;
	[SerializeField]
	private float GimickSpeed = 0.15f;
	[SerializeField]
	private float BackEndPosition = 10;
    //[SerializeField]
    //private GameObject gimmick;
    float count=0;
    int rand;
    bool gimmickSyokan = true;
    float transX;
    //private GameObject gimmickCar;
    //private GameObject gimmickRocket;
    // Use this for initialization
    void Start () {
        gimmickPosition = transform.position;
        /*
        gimmickCar = (GameObject)Resources.Load("prefabs/gimmick/gimmick_Car");
        gimmickRocket = (GameObject)Resources.Load("prefabs/gimmick/gimmick_Rocket");
        */
    }
	
	// Update is called once per frame
	void Update () {
        
        /*if (count >= 3){
            Instantiate(gimmickCar, gimmickPosition, Quaternion.identity);
            Instantiate(gimmickRocket, gimmickPosition, Quaternion.identity);
            count = 0;
        }
        gimmickRocket.transform.Translate(-1*GimickSpeed, 0, 0);
        gimmickCar.transform.Translate(-1 * GimickSpeed, 0, 0);
        */
        if (time.Count + 0.01 >= time.CountLimit)
        {
            transform.position = new Vector3(-25f, gimmickPosition.y, 0); ;
        }
        transform.Translate(-1 * GimickSpeed, 0, 0);
        if (GimickBack == true)
		{
            if (transform.position.x < (-BackEndPosition))
            {
                /*
                rand = Random.Range(0, 10);
                transX = rand <= 7 ? 25f : BackEndPosition + 25f;
                */
                transform.position = new Vector3(25f, gimmickPosition.y, 0);
            }
			/*if(time.Count <= time.CountLimit)
			{
				Destroy(gameObject);
			}*/
		}
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //どうしようか
    }
}
