using UnityEngine;
using System.Collections;

public class AimAt : MonoBehaviour
{ 
    public Vector3 aimAtLoc;
	void Update ()
    {
        if (gameObject.tag != "Inactive")   //if I can aim
        {
            transform.LookAt(aimAtLoc); //aim at whatever location I set
            transform.forward = new Vector3(transform.forward.x, 0.0f, transform.forward.z);    //zero out just in case if I didn't in other scripts
        }
	}
}
