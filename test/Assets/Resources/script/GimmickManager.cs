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
    // Use this for initialization
    void Start () {
        gimmickPosition = transform.position;
        
    }
	
	// Update is called once per frame
	void Update () {
		transform.Translate(-1*GimickSpeed, 0, 0);
		if (GimickBack == true)
		{
			if (transform.position.x < (BackEndPosition*-1))
			{
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
