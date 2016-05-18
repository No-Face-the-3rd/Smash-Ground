using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour
{
    #region Agent info
    private Vector3 startLoc, wanderLoc;
    private Transform tf;
    private Rigidbody rb;
    public float moveSpeed;
    public float wandTimer;
    private float originalTimer;
    #endregion

    // Use this for initialization
    void Start ()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        originalTimer = wandTimer;
        wanderLoc = startLoc = tf.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        wandTimer -= Time.deltaTime;
        tf.LookAt(wanderLoc);
        tf.forward = new Vector3(tf.forward.x, 0, tf.forward.z);
        rb.velocity = new Vector3(tf.forward.x * moveSpeed, 0.0f, tf.forward.z * moveSpeed);
        if (Vector3.Distance(wanderLoc, tf.position) < .25f || wandTimer <= 0)
        {
            wandTimer = originalTimer;
            startLoc = wanderLoc;
            wanderLoc.x = startLoc.x + (Random.insideUnitCircle.x * 5);
            wanderLoc.z = startLoc.z + (Random.insideUnitCircle.y * 5);
        }
    }
}
