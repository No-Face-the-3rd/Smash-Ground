using UnityEngine;
using System.Collections;

[RequireComponent (typeof (driveToTarget))]
public class Wander : MonoBehaviour
{
    #region Agent info
    private Vector3 startLoc;
    private driveToTarget wanderLoc;
    public float wandTimer;
    private float originalTimer;
    #endregion
    void Start ()
    {
        originalTimer = wandTimer;
        wanderLoc = GetComponent<driveToTarget>();
        startLoc = wanderLoc.targetLoc;
	}
	void Update ()
    {
        wandTimer -= Time.deltaTime;
        if (/*Vector3.Distance(wanderLoc.targetLoc, transform.position) < 0.80f ||*/ wandTimer <= 0)
        {
            wandTimer = originalTimer;
            startLoc = wanderLoc.targetLoc;
            wanderLoc.targetLoc.x = startLoc.x + (Random.insideUnitCircle.x * 5);
            wanderLoc.targetLoc.z = startLoc.z + (Random.insideUnitCircle.y * 5);
        }
    }
}

//DONE