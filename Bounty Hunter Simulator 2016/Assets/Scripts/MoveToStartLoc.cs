using UnityEngine;
using System.Collections;

public class MoveToStartLoc : MonoBehaviour
{

    private Vector3 startLoc;
    private Transform tf;
    private Rigidbody rb;
    public float returnSpeed;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponentInChildren<Rigidbody>();
        tf = GetComponent<Transform>();
        startLoc = tf.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(startLoc, tf.position) > .5f)
        {
            tf.LookAt(startLoc);
            tf.forward = new Vector3(tf.forward.x, 0, tf.forward.z);
            rb.velocity = new Vector3(tf.forward.x * returnSpeed,
                                0.0f, tf.forward.z * returnSpeed);
        }
        else
            rb.velocity = Vector3.zero;
    }
}
