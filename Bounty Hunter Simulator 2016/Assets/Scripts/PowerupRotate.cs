using UnityEngine;
using System.Collections;

public class PowerupRotate : MonoBehaviour {
    public float AnglePer;
	
	void Update ()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * AnglePer);
	}
}
