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
        float minDist = float.MaxValue; //used to see which player is closer
        int target = -1;                //start out without a target

        for (int i = 0; i < playerLocator.targetable.Length; ++i)
        {
            directionToPlayer = playerLocator.targetable[i].transform.position - transform.position; //find the direction to the current player
            float tmpDist = directionToPlayer.magnitude;    //distance between me and the player
            if (tmpDist < maxRadiusAvoid) //if within distance of radius
            {
                if (tmpDist < minDist) //if the new player position is closer, set minimum distance to tmpDist && set target to i
                {
                    minDist = tmpDist;
                    target = i;
                }
            }
        }

        MoveToStartLoc guard = GetComponent<MoveToStartLoc>();  //used for fixing previous bugs where the behaviors were additive
        Wander wandBehavior = GetComponent<Wander>();
 

        if(target >= 0) //if there is a target
        {
            if (guard != null)
                guard.enabled = false;
            if (wandBehavior != null)
                wandBehavior.enabled = false;
            AvoidPlayerDirection(target);
        }
        else
        {   //if not go back to whatever I was doing
            if (guard != null)
                guard.enabled = true;
            if (wandBehavior != null)
                wandBehavior.enabled = true;
        }
        
	}
    void AvoidPlayerDirection(int _target)  //used just to organize code more
    {
        playerDirection = (tf.position - playerLocator.targetable[_target].transform.position); //find the direction the player needs to be facing to be looking at me
        float angle = Vector3.Angle(playerDirection, playerLocator.targetable[_target].transform.forward);  //get the angle from the players forward and the desired direction

        if(angle < playerFieldOfViewAngle * 0.5f)   // if I am withing the desired directions threshold
        {
            avoidTimer -= Time.deltaTime;   //set a timer for quicker positional changes
            if (Vector3.Distance(travelLoc.targetLoc, tf.position) < 0.80f || avoidTimer <= 0) //have I reached my target position or ran out of time?
            {
                avoidTimer = originalTimer; //reset my avoid timer
                travelLoc.targetLoc.x = transform.position.x + (Random.insideUnitCircle.x * 5); //new random target position
                travelLoc.targetLoc.z = transform.position.z + (Random.insideUnitCircle.y * 5);
            }
        }
        else
            travelLoc.targetLoc = transform.position; // if I am not in the players view, set my target position to my current position so I don't continuously move towards it
    }
}

//DONE
//vector3.angle
//pass in two vectors, vector to the player && player forward 
// if this angle is < half the FOV angle then the enemy is within the field of view