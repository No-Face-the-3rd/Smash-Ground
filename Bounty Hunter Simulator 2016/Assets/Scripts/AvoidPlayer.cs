using UnityEngine;
using System.Collections;

public class AvoidPlayer : MonoBehaviour
{
    private GameObject[] players;
    private Transform tf;
    private Rigidbody rb;
    public float maxRange;
    public float moveSpeed;
    public float playerFieldOfViewAngle;
    private Vector3 playerDirection;

    public GameObject test;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
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
            float startRad = Vector3.Distance(tf.position, players[i].transform.position);
            float tmpDist = Vector3.Distance(tf.position, players[i].transform.position);
            if (startRad < maxRange) //if within distance of radius, range too large
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
            GameObject tmp = (GameObject)Instantiate(test, tf.position + tf.forward * 0.5f + new Vector3(0.0f,1,0.0f),
                    Quaternion.LookRotation(tf.forward));
            tmp.layer = 11;
        }
    }
}

//vector3.angle
//pass in two vectors, vector to the player && player forward 
// if this angle is < half the FOV angle then the enemy is within the field of view