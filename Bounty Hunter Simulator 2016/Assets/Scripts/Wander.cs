using UnityEngine;
using System.Collections;

[RequireComponent (typeof (driveToTarget))]
public class Wander : MonoBehaviour
{
    #region Agent info

    private Vector3 startLoc;
    private driveToTarget wanderLoc;
    private AimAt aim;
    public float wandTimer;
    private float originalTimer;

    #endregion
    void Start ()
    {
        originalTimer = wandTimer;
        wanderLoc = GetComponent<driveToTarget>();
        startLoc = wanderLoc.targetLoc;
        aim = GetComponent<AimAt>();
	}
	void Update ()
    {
        wandTimer -= Time.deltaTime;    //uses a timer for better wandering(won't get stuck as easy)
        if (Vector3.Distance(wanderLoc.targetLoc, transform.position) < 0.80f || wandTimer <= 0)    //Am I at the loction I needed to travel to or did it take to long?
        {
            wandTimer = originalTimer;      //reset the wander timer
            startLoc = transform.position; //set my previous location to my location
            wanderLoc.targetLoc.x = startLoc.x + (Random.insideUnitCircle.x * 5); // select a point based on my previous position
            wanderLoc.targetLoc.z = startLoc.z + (Random.insideUnitCircle.y * 5);
        }

        aim.aimAtLoc = wanderLoc.targetLoc; //Aim at my current traveling location
    }
}