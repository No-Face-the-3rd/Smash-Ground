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
        moveTo = GetComponent<driveToTarget>();
        aim = GetComponent<AimAt>();
        if (this.gameObject.tag == "Spawning")
        {
            startLoc = moveTo.targetLoc;
        }
    }
	
	void Update ()
    {
        //drive to handles the logic
        moveTo.targetLoc = startLoc;
        aim.aimAtLoc = startLoc;
    }
}
