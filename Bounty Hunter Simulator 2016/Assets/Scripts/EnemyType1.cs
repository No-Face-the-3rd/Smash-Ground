using UnityEngine;
using System.Collections;

public class EnemyType1 : MonoBehaviour
{
    //movement finite state machine
    enum ControlState {STAND, WALK };
    private ControlState movementState;

    //Agent info
    private Vector3 startLoc, targetLoc;
    private Rigidbody rb;
    private Transform tf;
    public GameObject player;
    public float moveSpeed;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        movementState = ControlState.STAND;
        tf.forward = Vector3.zero;
       
    }
	
	// Update is called once per frame
	void Update ()
    {
        MovementBehavior();
    }
    void MovementBehavior()
    {
        switch(movementState)
        {
            case ControlState.STAND:
                startLoc = tf.position;
                break;

            case ControlState.WALK:
                targetLoc = player.transform.position;
//                rb.velocity = Vector3.Normalize(new Vector3(, 0, )) * moveSpeed;
                break;
        }
    }
}
