using UnityEngine;
using System.Collections;

public class MoveToStartLoc : MonoBehaviour
{

    private Vector3 startLoc;
    private Transform tf;
    public float returnSpeed;

	// Use this for initialization
	void Start ()
    {
        tf = GetComponent<Transform>();
        startLoc = tf.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(startLoc, tf.position) > .5f)
        {
            tf.LookAt(startLoc);
            tf.position += tf.forward * returnSpeed * Time.deltaTime;
        }
    }
}
