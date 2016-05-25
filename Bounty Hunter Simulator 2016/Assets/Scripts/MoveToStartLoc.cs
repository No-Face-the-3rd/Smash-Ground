using UnityEngine;
using System.Collections;

[RequireComponent (typeof(driveToTarget))]
public class MoveToStartLoc : MonoBehaviour
{

    private Vector3 startLoc;
    private driveToTarget moveTo;

	// Use this for initialization
	void Start ()
    {
        moveTo = GetComponentInChildren<driveToTarget>();
        startLoc = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        moveTo.targetLoc = startLoc;
    }
}

//DONE