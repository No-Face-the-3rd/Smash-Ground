using UnityEngine;
using System.Collections;

public class AimAt : MonoBehaviour
{

    public Vector3 aimAtLoc;
	// Use this for initialization
	void Start ()
    {
	    if(aimAtLoc == Vector3.zero)
        {
            aimAtLoc = transform.forward;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(aimAtLoc);
	}
}
