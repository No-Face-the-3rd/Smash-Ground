using UnityEngine;
using System.Collections;

[RequireComponent (typeof(driveToTarget))]
public class AvoidPlayer : MonoBehaviour
{
    private GameObject[] players;
    private Transform tf;
    public float maxRadiusForAvoid;
    public float playerFieldOfViewAngle;
    private Vector3 playerDirection, directionToPlayer;
    private driveToTarget travelLoc;

	// Use this for initialization
	void Start ()
    {
        travelLoc = GetComponent<driveToTarget>();
        tf = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        players = GameObject.FindGameObjectsWithTag("Player"); //get the active players
        float minDist = float.MaxValue;
        int target = -1;
        for (int i = 0; i < players.Length; ++i)
        {
            directionToPlayer = players[i].transform.position - transform.position;
            float tmpDist = directionToPlayer.magnitude;
            if (tmpDist < maxRadiusForAvoid) //if within distance of radius, range too large
            {
                if (tmpDist < minDist) //if the new player position is closer, set minimum distance to tmpDist && set target to i
                {
                    minDist = tmpDist;
                    target = i;
                }
            }
        }

        if(target >= 0)
        {
            AvoidPlayerDirection(target);
        }
        
	}

    void AvoidPlayerDirection(int _target)
    {
        playerDirection = (tf.position - players[_target].transform.position);
        float angle = Vector3.Angle(playerDirection, players[_target].transform.forward);

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