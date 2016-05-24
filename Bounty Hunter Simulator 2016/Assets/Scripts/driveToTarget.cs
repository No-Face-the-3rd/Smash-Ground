using UnityEngine;
using System.Collections;

public class driveToTarget : MonoBehaviour {

    public Vector3 targetLoc;
    public float moveSpeed;
    private Rigidbody rb;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        if (targetLoc == Vector3.zero)
        {
            targetLoc = transform.position;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(targetLoc, transform.position) > 0.50f)
        {
            transform.LookAt(targetLoc);
            transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
            rb.velocity = new Vector3(transform.forward.x * moveSpeed, 0.0f, transform.forward.z * moveSpeed);
        }
    }
}

//DONE