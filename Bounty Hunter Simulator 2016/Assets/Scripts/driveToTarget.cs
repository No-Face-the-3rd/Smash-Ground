using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class driveToTarget : MonoBehaviour
{
    public Animator anim;
    public Vector3 targetLoc;
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
        if (Vector3.Distance(targetLoc, transform.position) > 0.80f)
        {
            anim.Play("walking", -1, 0f);
            transform.LookAt(targetLoc);
            transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
            rb.velocity = new Vector3(transform.forward.x * moveSpeed, 0.0f, transform.forward.z * moveSpeed);
        }
    }
}

