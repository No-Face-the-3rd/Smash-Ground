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
        if (targetLoc == Vector3.zero)
        {
            targetLoc = transform.position;
        }
	}
	void Update ()
    {
        if (gameObject.tag != "Inactive")
        {
            directionToTarget = targetLoc - transform.position;
            if (directionToTarget.magnitude >= 1f)
            {
                rb.velocity = new Vector3(directionToTarget.normalized.x * moveSpeed, 
                                          rb.velocity.y, 
                                          directionToTarget.normalized.z * moveSpeed);
            }
            else
            {
                if(gameObject.tag == "Spawning")
                {
                    gameObject.tag = "Active";
                }
            }
           
        }
    }
}

