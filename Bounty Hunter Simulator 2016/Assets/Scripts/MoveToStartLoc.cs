using UnityEngine;
using System.Collections;

[RequireComponent (typeof(driveToTarget))]
public class MoveToStartLoc : MonoBehaviour
{
    private Vector3 startLoc, startAimLoc;
    private driveToTarget moveTo;
    private AimAt aim;

    void Start()
    {
        moveTo = GetComponent<driveToTarget>();
        aim = GetComponent<AimAt>();

    }

    void Update()
    {
        if (this.gameObject.tag == "Spawning")
        {
            startLoc = moveTo.targetLoc;
            startAimLoc = aim.aimAtLoc;
        }
        //drive to handles the logic
        moveTo.targetLoc = startLoc;
        aim.aimAtLoc = startAimLoc;
    }
}
