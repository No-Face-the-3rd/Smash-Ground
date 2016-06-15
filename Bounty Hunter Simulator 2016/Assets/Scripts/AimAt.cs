using UnityEngine;
using System.Collections;

public class AimAt : MonoBehaviour
{ 
    public Vector3 aimAtLoc;
	void Update ()
    {
        if (gameObject.tag != "Inactive")
        {
            transform.LookAt(aimAtLoc);
            transform.forward = new Vector3(transform.forward.x, 0.0f, transform.forward.z);
        }
	}
}
