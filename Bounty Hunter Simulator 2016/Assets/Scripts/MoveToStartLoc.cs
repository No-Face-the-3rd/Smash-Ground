using UnityEngine;
using System.Collections;

[RequireComponent (typeof(driveToTarget))]
public class MoveToStartLoc : MonoBehaviour
{
    private Vector3 startLoc;
    private driveToTarget moveTo;
    private AimAt aim;

	void Start ()
    {
        moveTo = GetComponentInChildren<driveToTarget>();
        aim = GetComponentInChildren<AimAt>();
        startLoc = transform.position;
	}
	
	void Update ()
    {
        moveTo.targetLoc = startLoc;
        aim.aimAtLoc = startLoc;
    }
}

//DONE