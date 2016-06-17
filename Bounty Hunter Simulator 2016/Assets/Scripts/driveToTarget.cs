using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class driveToTarget : MonoBehaviour
{
    public Vector3 targetLoc, directionToTarget;
    public float moveSpeed;
    private Rigidbody rb;
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	void Update ()
    {
        if (gameObject.tag != "Inactive")   //if the enemy should be moving
        {
            directionToTarget = targetLoc - transform.position; 
            if (directionToTarget.magnitude >= 1f)  //if I am not at my location
            {
                rb.velocity = new Vector3(directionToTarget.normalized.x * moveSpeed, 
                                          rb.velocity.y, 
                                          directionToTarget.normalized.z * moveSpeed);  //then move towards it
            }
            else
            {
                if(gameObject.tag == "Spawning") //I've reached my destination and Im spawning?
                {
                    gameObject.tag = "Active";  //I can do my other behaviors now
                }
            }
           
        }
    }
}

