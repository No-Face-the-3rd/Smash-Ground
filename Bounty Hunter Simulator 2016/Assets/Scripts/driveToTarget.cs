using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class driveToTarget : MonoBehaviour
{
    public Animator anim;
    public Vector3 targetLoc, directionToTarget;
    public float moveSpeed;
    private Rigidbody rb;
    void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        if (targetLoc == Vector3.zero)
        {
            targetLoc = transform.position;
        }
	}
	void Update ()
    {
        if (gameObject.tag == "Active")
        {
            directionToTarget = Vector3.Normalize(targetLoc - transform.position);
            if (directionToTarget.magnitude >= 0.80f)
            {
                rb.velocity = new Vector3(directionToTarget.x * moveSpeed, rb.velocity.y, directionToTarget.z * moveSpeed);
            }
        }
    }
}

