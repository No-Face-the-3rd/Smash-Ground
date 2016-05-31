using UnityEngine;
using System.Collections;

[RequireComponent (typeof(driveToTarget))]
public class MoveToStartLoc : MonoBehaviour
{
    private Vector3 startLoc;
    private driveToTarget moveTo;

	void Start ()
    {
        moveTo = GetComponentInChildren<driveToTarget>();
        startLoc = transform.position;
	}
	
	void Update ()
    {
        moveTo.targetLoc = startLoc;
    }
}

//DONE