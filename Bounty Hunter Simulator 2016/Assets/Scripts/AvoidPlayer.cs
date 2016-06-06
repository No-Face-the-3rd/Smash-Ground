using UnityEngine;
using System.Collections;

[RequireComponent (typeof(driveToTarget))]
public class AvoidPlayer : MonoBehaviour
{
    #region Agent Info

    public PlayerLocator playerLocator;
    private Transform tf;
    public float avoidTimer;
    private float originalTimer;
    public float maxRadiusAvoid;
    public float playerFieldOfViewAngle;
    private Vector3 playerDirection, directionToPlayer;
    private driveToTarget travelLoc;

    #endregion
    void Start ()
    {
        travelLoc = GetComponent<driveToTarget>();
        tf = GetComponent<Transform>();
        playerLocator = GameObject.FindObjectOfType<PlayerLocator>();
        originalTimer = avoidTimer;
    }
	void Update ()
    {
        float minDist = float.MaxValue;
        int target = -1;

        for (int i = 0; i < playerLocator.targetable.Length; ++i)
        {
            directionToPlayer = playerLocator.targetable[i].transform.position - transform.position;
            float tmpDist = directionToPlayer.magnitude;
            if (tmpDist < maxRadiusAvoid) //if within distance of radius, range too large
            {
                if (tmpDist < minDist) //if the new player position is closer, set minimum distance to tmpDist && set target to i
                {
                    minDist = tmpDist;
                    target = i;
                }
            }
        }

        MoveToStartLoc guard = GetComponent<MoveToStartLoc>();
        Wander wandBehavior = GetComponent<Wander>();
 

        if(target >= 0)
        {
            if (guard != null)
                guard.enabled = false;
            if (wandBehavior != null)
                wandBehavior.enabled = false;
            AvoidPlayerDirection(target);
        }
        else
        {
            if (guard != null)
                guard.enabled = true;
            if (wandBehavior != null)
                wandBehavior.enabled = true;
        }
        
	}
    void AvoidPlayerDirection(int _target)
    {
        playerDirection = (tf.position - playerLocator.targetable[_target].transform.position);
        float angle = Vector3.Angle(playerDirection, playerLocator.targetable[_target].transform.forward);

        if(angle < playerFieldOfViewAngle * 0.5f)
        {
            avoidTimer -= Time.deltaTime;
            if (Vector3.Distance(travelLoc.targetLoc, tf.position) < 0.80f || avoidTimer <= 0)
            {
                avoidTimer = originalTimer;
                travelLoc.targetLoc.x = transform.position.x + (Random.insideUnitCircle.x * 5);
                travelLoc.targetLoc.z = transform.position.z + (Random.insideUnitCircle.y * 5);
            }
        }
        else
            travelLoc.targetLoc = transform.position;
    }
}

//DONE
//vector3.angle
//pass in two vectors, vector to the player && player forward 
// if this angle is < half the FOV angle then the enemy is within the field of view