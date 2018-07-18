using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionMane : MonoBehaviour {
    [SerializeField]
    static GameObject WP;

    public float GetXPosition()
    {
        return WP.transform.position.x;
    }
    public float GetYPosition()
    {
        return WP.transform.position.y;
    }
    public float GetZPosition()
    {
        return WP.transform.position.z;
    }
}
