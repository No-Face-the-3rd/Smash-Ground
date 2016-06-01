using UnityEngine;
using System.Collections;

[RequireComponent (typeof(driveToTarget))]
public class AvoidPlayer : MonoBehaviour
{
    #region Agent Info

    public PlayerLocator playerLocator;
    private Transform tf;
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
    }
	void Update ()
    {
        float minDist = float.MaxValue;
        int target = -1;
        for (int i = 0; i < playerLocator.players.Length; ++i)
        {
            directionToPlayer = playerLocator.players[i].transform.position - transform.position;
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
        AvoidPlayerDirection(target);

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
        playerDirection = (tf.position - playerLocator.players[_target].transform.position);
        float angle = Vector3.Angle(playerDirection, playerLocator.players[_target].transform.forward);

        if(angle < playerFieldOfViewAngle * 0.5f)
        {
            if(Vector3.Distance(travelLoc.targetLoc, tf.position) < 0.80f)
            {
                travelLoc.targetLoc.x = transform.position.x + (Random.insideUnitCircle.x * 5);
                travelLoc.targetLoc.z = transform.position.z + (Random.insideUnitCircle.y * 5);
            }
        }
    }
}

//DONE
//vector3.angle
//pass in two vectors, vector to the player && player forward 
// if this angle is < half the FOV angle then the enemy is within the field of view